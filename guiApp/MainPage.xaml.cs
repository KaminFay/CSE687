/*
 * MainPage.xaml.cs - Many Entry point for the C# GUI
 * Includes all of the GUI control code as well
 * 
 * Language:    C#, VS 2019
 * Platform:    Windows 10 (UWP)
 * Application: CSE687 Project
 * Author:      Kamin Fay       -- kfay02@syr.edu
 *              Brandon Hancock -- behancoc@syr.edu
 *              Austin Cassidy  -- aucassid@syr.edu
 *              Ralph Walker    -- rwalkeri@syr.edu
 */
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace guiApp
{

    public sealed partial class MainPage : Page
    {
        /*
         * ----< Description > 
         * Extension is a simple enumerated data type to allow us to switch 
         * between DLL / JSON in the file picker; DLL = 0, JSON = 1
         * This is included for readability.
         * ----< Description >
         */
        public enum Extension {
            DLL,
            JSON
        };

        /*
        * ----< Description > 
        * Group of all collections, and objects that are required for the GUI.
        * ----< Description >
        */
        private ObservableCollection<StorageFile> fileList = new ObservableCollection<StorageFile>();
        private ObservableCollection<dllInfo> fileNamesForListView = new ObservableCollection<dllInfo>();
        private ObservableCollection<dllFunction> functionForListView = new ObservableCollection<dllFunction>();
        private ObservableCollection<dllFunction> functionsToHarness = new ObservableCollection<dllFunction>();
        private JSONParser jsonParser = new JSONParser();
        private GuiLogger logger;

        /*
         * ----< Function > MainPage
         * ----< Description > 
         * Program entry point, initializes the GUI and logger.
         * Also clears the API for usage during the current session.
         * ----< Description >
         * @Param None
         * @Return None
         */
        public MainPage()
        {
            this.InitializeComponent();
            API_Interface.TruncateTestFunctions(); // Will be used to remove any sort of misplaced data on the API side
            logger = new GuiLogger("Initializing Logger", ref this.Logger, ref this.logScrollViewer);
            logger.AddLogMessage("Initializing GUI");
        }

        private void OnElementClicked(Object sender, RoutedEventArgs routedEventArgs)
        {
            Debug.WriteLine("Item clicked!");
        }



        private void CloseApp(Object sender, RoutedEventArgs routedEventArgs)
        {

            DisplayCloseApplicationDialog();
        }


        private async void DisplayCloseApplicationDialog()
        {
            ContentDialog closeAppDialog = new ContentDialog
            {
                Title = "Would you like to close the application?",
                
                CloseButtonText = "Cancel",
                PrimaryButtonText = "Close",
                DefaultButton = ContentDialogButton.Primary
            };

            ContentDialogResult result = await closeAppDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Application.Current.Exit();
            }
        }

        /*
         * ----< Function > OpenFilePicker_Click
         * ----< Description > 
         * When a new file is needed this will call the file selector function and let it know
         * we need to open a JSON file.
         * ----< Description >
         * @Param None
         * @Return None
         */
        private async void OpenFilePicker_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            FileSelector(Extension.JSON);

        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /*
         * ----< Function > FileSelector
         * ----< Description > 
         * Open a file, depending on type passed in the file selector will be filtered.
         * ----< Description >
         * @Param Extension extension -- The extension type for the file we are looking for
         * @Return None
         */
        private async void FileSelector(Extension extension)
        {
            FileOpenPicker openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.Desktop
            };

            if (extension == Extension.DLL)
            {
                openPicker.FileTypeFilter.Add(".dll");
                IReadOnlyList<StorageFile> files = await openPicker.PickMultipleFilesAsync();
                if (files.Count > 0)
                {
                    StringBuilder output = new StringBuilder("Selected DLLs: \n");

                    foreach (StorageFile file in files)
                    {
                        output.Append(file.DisplayName + "\n");
                        fileList.Add(file);
                    }
                }
                else
                {
                }
            }
            else if(extension == Extension.JSON)
            {
                openPicker.FileTypeFilter.Add(".json");
                StorageFile storageFile = await openPicker.PickSingleFileAsync();

                if(storageFile != null)
                {
                    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(storageFile);
                    logger.AddOpenFileMessage(storageFile.DisplayName + ".json", storageFile.Path);
                    AddDLLsToGUI(storageFile);
                }
            }
        }

        /*
         * ----< Function > BindDLL
         * ----< Description > 
         * Once a dll is selected this function will bind the physical file and return
         * a dllBindingClass containing the path / filename data.
         * ----< Description >
         * @Return dllBindingClass -- the data for binding
         */
        private async Task<dllBindingClass> BindDLL()
        {
            FileOpenPicker openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.Desktop
            };
            openPicker.FileTypeFilter.Add(".dll");
            StorageFile sFile = await openPicker.PickSingleFileAsync();

            if(sFile != null)
            {
                Debug.WriteLine("Testing the printing of the DLL Path");
                Debug.WriteLine(sFile.Path);
                logger.AddOpenFileMessage(sFile.DisplayName + ".dll", sFile.Path);
                return new dllBindingClass(sFile.Path, sFile.DisplayName + ".dll");
            }
            return null;
        }

        /*
         * ----< Function > AddDLLsToGUI
         * ----< Description > 
         * Given a a JSON file this function will add the contents of that file to the GUI
         * ----< Description >
         * @Param StorageFile storageFile -- file pointer to the JSON containing DLL data.
         */
        public async void AddDLLsToGUI(StorageFile storageFile)
        {
            List<dllInfo> allDLLData = await jsonParser.readInJSON(storageFile);                // Read in the JSON and return a list of the DLL's that are contained (asynchronously)

            fileList.Add(storageFile);
            foreach (dllInfo dll in allDLLData)
            {
                //If the list is empty by default add the new dll to the list
                if (fileNamesForListView.Count == 0)
                {
                    fileNamesForListView.Add(dll);
                    this.Items.ItemsSource = fileNamesForListView;
                }
                else
                {
                    //Check to see if the dll exists in the list, if it doesn't add it
                    //This prevent's any duplicate DLL's that may be in separate JSON files that are loaded
                    if (!fileNamesForListView.Any(d => d.dllName == dll.dllName))
                    {
                        fileNamesForListView.Add(dll);
                        this.Items.ItemsSource = fileNamesForListView;
                    }
                }
            }
        }

        /*
         * ----< Function > DllToggled
         * ----< Description > 
         * Once a DLL is toggled the application will call BindDLL(), open a file picker,
         * select the file and bind the physical file to the logic here
         * ----< Description >
         */
        private async void DllToggled(object sender, RoutedEventArgs e)
        {
            int indexOfCurrentDLLToggled;
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            dllInfo sampleDll = (dllInfo)((Grid)toggleSwitch.Parent).DataContext;
            Debug.WriteLine(sampleDll.dllName + " Was toggled");

            if (toggleSwitch.IsOn)
            {
                dllBindingClass dllBinder = await BindDLL();

                if(dllBinder != null)
                {
                    Debug.WriteLine("Testing the binder");
                    Debug.WriteLine(dllBinder.dllFullPath + "---" + dllBinder.dllName);

                    Debug.WriteLine("------------");
                    Debug.WriteLine("Before the move: ");
                    indexOfCurrentDLLToggled = fileNamesForListView.IndexOf(sampleDll);
                    if(fileNamesForListView.ElementAt<dllInfo>(indexOfCurrentDLLToggled).dllName == dllBinder.dllName)
                    {
                        Debug.WriteLine(fileNamesForListView.ElementAt<dllInfo>(indexOfCurrentDLLToggled).dllLocation);
                        fileNamesForListView.ElementAt<dllInfo>(indexOfCurrentDLLToggled).dllLocation = dllBinder.dllFullPath;
                        Debug.WriteLine("After the move: ");
                        Debug.WriteLine(fileNamesForListView.ElementAt<dllInfo>(indexOfCurrentDLLToggled).dllLocation);
                    }
                    foreach (dllFunction function in sampleDll.functionList)
                    {
                        function.DllName = fileNamesForListView.ElementAt<dllInfo>(indexOfCurrentDLLToggled).dllName;
                        function.DllPath = fileNamesForListView.ElementAt<dllInfo>(indexOfCurrentDLLToggled).dllLocation;
                        functionForListView.Add(function);
                        this.FunctionList.ItemsSource = functionForListView;
                    }
                }

            }
            else
            {
                functionForListView.Clear();
                this.FunctionList.ItemsSource = functionForListView;
            }
        }

        /*
         * ----< Function > FunctionToggled
         * ----< Description > 
         * Once a function is toggled it will be added into a list of functions that
         * are to be sent to the test harness backend.
         * ----< Description >
         */
        private void FunctionToggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            dllFunction sampleFunction = (dllFunction)((Grid)toggleSwitch.Parent).DataContext;

            if (toggleSwitch.IsOn)
            {
                functionsToHarness.Add(sampleFunction);
                this.TestableList.ItemsSource = functionsToHarness;
            }
            else
            {
                functionsToHarness.Remove(sampleFunction);
                this.TestableList.ItemsSource = functionsToHarness;
            }
        }

        /*
         * ----< Function > Run_Test_Execution
         * ----< Description > 
         * Clicking Run will take the testable items selected, send them to the harness
         * and wait for responses, both of which are handled asynchronously.
         * ----< Description >
         */
        private async void Run_Test_Execution(object sender, RoutedEventArgs e)
        {
            foreach(dllFunction func in functionsToHarness)
            {
                Debug.WriteLine("Sending Another");
                JObject jObject = JSONParser.dllFunctionToJSON(func);
                var ID = await API_Interface.PostTestFunctionAsync(jObject);
                logger.PostedTestFunctionLog(jObject, ID);
                Debug.WriteLine("Items in functionIDsSent: " + API_Interface.functionsIDsSent.Count());
            }
            API_Interface.GetResultsAsync(logger);
        }

        private void Items_ItemClick(object sender, ItemClickEventArgs e)
        {
            Debug.WriteLine("Item is clicked");
        }

        /*
         * ----< Function > Cancel_Button_Click
         * ----< Description > 
         * Clear all of the data that has been loaded already along with the GUI.
         * ----< Description >
         */
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            fileList.Clear();
            fileNamesForListView.Clear();
            functionForListView.Clear();
            functionsToHarness.Clear();
            this.TestableList.ItemsSource = null;
            this.FunctionList.ItemsSource = null;
            this.Items.ItemsSource = null;
        }

        /*
         * ----< Function > Level_Three_Button_Checked
         * ----< Description > 
         * Set the logging level to 3, and reset the other two buttons.
         * ----< Description >
         */
        private void Level_Three_Button_Checked(object sender, RoutedEventArgs e)
        {
            GuiLogger.SetLogLevel(3);
            this.Level_One_Button.IsChecked = false;
            this.Level_Two_Button.IsChecked = false;
        }

        /*
         * ----< Function > Level_Two_Button_Checked
         * ----< Description > 
         * Set the logging level to 2, and reset the other two buttons.
         * ----< Description >
         */
        private void Level_Two_Button_Checked(object sender, RoutedEventArgs e)
        {
            GuiLogger.SetLogLevel(2);
            this.Level_One_Button.IsChecked = false;
            this.Level_Three_Button.IsChecked = false;

        }

        /*
         * ----< Function > Level_One_Button_Checked
         * ----< Description > 
         * Set the logging level to 1, and reset the other two buttons.
         * ----< Description >
         */
        private void Level_One_Button_Checked(object sender, RoutedEventArgs e)
        {
            GuiLogger.SetLogLevel(1);
            this.Level_Three_Button.IsChecked = false;
            this.Level_Two_Button.IsChecked = false;
        }
    }
}
