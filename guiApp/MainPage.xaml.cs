using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Collections.ObjectModel;
using System.Diagnostics;
using guiApp.HelperClasses;


using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace guiApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 


    public sealed partial class MainPage : Page
    {
        public enum Extension {
            DLL,
            JSON
        };

        //System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();


        private ObservableCollection<StorageFile> fileList = new ObservableCollection<StorageFile>();
        private ObservableCollection<dllInfo> fileNamesForListView = new ObservableCollection<dllInfo>();
        private ObservableCollection<dllFunction> functionForListView = new ObservableCollection<dllFunction>();
        private ObservableCollection<DLLObject> sendToTestHarness = new ObservableCollection<DLLObject>();
        private ObservableCollection<dllFunction> functionsToHarness = new ObservableCollection<dllFunction>();
        private JSONParser jsonParser = new JSONParser();
        private SendingSocket ss = new SendingSocket("127.0.0.1", 8080);

        public MainPage()
        {
            this.InitializeComponent();
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

        private async void OpenFilePicker_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            fileSelector(Extension.JSON);

        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            //var items = new ObservableCollection<DLLObject>();
            //for(int i = 0; i < 9; i ++)
            //{
            //    items.Add(new DLLObject($"item {i}"));
            //}
            //this.Items.ItemsSource = items;
        }

        private async void fileSelector(Extension extension)
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
                        Debug.WriteLine("File name is: " + file.DisplayName);
                        Debug.WriteLine("File display type: " + file.DisplayType);
                        Debug.WriteLine("File type: " + file.FileType);
                        Debug.WriteLine("File properties: " + file.Properties);
                        Debug.WriteLine("File path: " + file.Path);

                        output.Append(file.DisplayName + "\n");
                        fileList.Add(file);

                        //fileNamesForListView.Add(file.DisplayName + file.FileType);
                    }
                    //this.Items.ItemsSource = fileNamesForListView;
                }
                else
                {
                    //OutputTextBlock.Text = "Operation cancelled.";
                }
            }
            else if(extension == Extension.JSON)
            {
                openPicker.FileTypeFilter.Add(".json");
                StorageFile storageFile = await openPicker.PickSingleFileAsync();

                if(storageFile != null)
                {

                    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(storageFile);
                    addDLLsToGUI(storageFile);
                }
            }
        }

        private async Task<dllBindingClass> bindDLL()
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
                return new dllBindingClass(sFile.Path, sFile.DisplayName + ".dll");
            }
            return null;
        }

        public async void addDLLsToGUI(StorageFile storageFile)
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

        private async void dllToggled(object sender, RoutedEventArgs e)
        {
            //fileSelector(Extension.DLL);

            int indexOfCurrentDLLToggled;
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            dllInfo sampleDll = (dllInfo)((Grid)toggleSwitch.Parent).DataContext;
            Debug.WriteLine(sampleDll.dllName + " Was toggled");

            if (toggleSwitch.IsOn)
            {
                dllBindingClass dllBinder = await bindDLL();

                if(dllBinder != null)
                {
                    Debug.WriteLine("Testing the binder");
                    Debug.WriteLine(dllBinder.dllFullPath + "---" + dllBinder.dllName);
                    //Hi my name is michaela and I like this keyboard and I will be back to steal it

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
                    //String pathLocation = returnTask.Result;
                    foreach (dllFunction function in sampleDll.functionList)
                    {
                        function.dllName = fileNamesForListView.ElementAt<dllInfo>(indexOfCurrentDLLToggled).dllName;
                        function.dllPath = fileNamesForListView.ElementAt<dllInfo>(indexOfCurrentDLLToggled).dllLocation;
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


            //var toggle = (ToggleSwitch) sender;
            //var dataContext = ((Grid)toggle.Parent).DataContext;
            //var dataItem =  (dllInfo) dataContext;
            //dataItem.DLLObjectName = $"Toggled {toggle.IsOn}";

            //if (toggle.IsOn)
            //{
            //    sendToTestHarness.Add(dataItem);
            //}

            //if (!toggle.IsOn)
            //{
            //    sendToTestHarness.Remove(dataItem);
            //}

            //Debug.WriteLine("The current size of the list is: " + sendToTestHarness.Count);
        }

        private async void functionToggled(object sender, RoutedEventArgs e)
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


        private void RichEditBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Run_Test_Execution(object sender, RoutedEventArgs e)
        {
            //Queue<DLLObject> harnessQueue = new Queue<DLLObject>();

            //foreach (DLLObject dLLObject in sendToTestHarness)
            //{
            //    harnessQueue.Enqueue(dLLObject);
            //}

            //Debug.WriteLine("The current size of the queue is: " + harnessQueue.Count);
            ss.EstablishConnection();
            foreach(dllFunction func in functionsToHarness)
            {
                Debug.WriteLine("Sending Another");
                BufferBuilder builder = new BufferBuilder(func);
                builder.SerializeAndSendBuffer(ss);
            }
            ss.CleanSocket();
        }

        private void Items_ItemClick(object sender, ItemClickEventArgs e)
        {
            Debug.WriteLine("Item is clicked");
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            fileList.Clear();
            fileNamesForListView.Clear();
            functionForListView.Clear();
            sendToTestHarness.Clear();
            functionsToHarness.Clear();
            this.TestableList.ItemsSource = null;
            this.FunctionList.ItemsSource = null;
            this.Items.ItemsSource = null;
        }
    }
}
