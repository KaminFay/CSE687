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

        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();


        private ObservableCollection<StorageFile> fileList = new ObservableCollection<StorageFile>();
        private ObservableCollection<String> fileNamesForListView = new ObservableCollection<string>();
        private ObservableCollection<DLLObject> sendToTestHarness = new ObservableCollection<DLLObject>();

        public MainPage()
        {
            this.InitializeComponent();
           
        }

        private void OnElementClicked(Object sender, RoutedEventArgs routedEventArgs)
        {
            Console.WriteLine("Item clicked!");
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

                        fileNamesForListView.Add(file.DisplayName + file.FileType);
                    }
                    this.Items.ItemsSource = fileNamesForListView;
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
                    JSONParser jsonParser = new JSONParser(storageFile.DisplayName, storageFile.Path);
                    jsonParser.readInJSON(storageFile);
                    fileList.Add(storageFile);

                    fileNamesForListView.Add(storageFile.DisplayName + storageFile.FileType);
                    this.Items.ItemsSource = fileNamesForListView;
                }
            }
        }

        private async void Toggle_Toggled(object sender, RoutedEventArgs e)
        {
            fileSelector(Extension.DLL);


            ToggleSwitch toggleSwitch = sender as ToggleSwitch;



            //var toggle = (ToggleSwitch) sender;
            //var dataContext = ((Grid)toggle.Parent).DataContext;
            //var dataItem =  (DLLObject) dataContext;
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

        private void RichEditBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Run_Test_Execution(object sender, RoutedEventArgs e)
        {
            Queue<DLLObject> harnessQueue = new Queue<DLLObject>();

            foreach (DLLObject dLLObject in sendToTestHarness)
            {
                harnessQueue.Enqueue(dLLObject);
            }

            Debug.WriteLine("The current size of the queue is: " + harnessQueue.Count);
        }
    }
}
