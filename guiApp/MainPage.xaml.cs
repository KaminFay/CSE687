﻿using System;
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
    public sealed partial class MainPage : Page
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();


        private ObservableCollection<StorageFile> fileList = new ObservableCollection<StorageFile>();
        private ObservableCollection<String> fileNamesForListView = new ObservableCollection<string>();

        public MainPage()
        {
            this.InitializeComponent();

            //try
            //{
            //    Console.WriteLine("Connect to server");
            //    clientSocket.Connect("127.0.0.1", 8888);
            //    Console.WriteLine("Connected to server??????????");

            //    NetworkStream serverStream = clientSocket.GetStream();
            //    byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Hello World??\n");

            //    serverStream.Write(outStream, 0, outStream.Length);
            //    serverStream.Flush();

            //    byte[] inStream = new byte[10025];
            //    //serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
            //    serverStream.Read(inStream, 0, inStream.Length);
            //    string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            //    //msg(returndata);


            //} catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}

            byte[] bytes = new byte[1024];

            try
            {
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP  socket.    
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.    
                try
                {
                    // Connect to Remote EndPoint  
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.    
                    byte[] msg = Encoding.ASCII.GetBytes("Hello World! <EOF>");

                    // Send the data through the socket.    
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.    
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Release the socket.    
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }



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
            // Clear any previously returned files between iterations of this scenario
            //OutputTextBlock.Text = "";

            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add("*");
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

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Toggle_Toggled(object sender, RoutedEventArgs e)
        {
            DLLObject temp = new DLLObject("Test");
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;

            if(toggleSwitch.IsOn)
            {
                Debug.WriteLine("I was toggled!");
                //IEnumerator<DLLObject> enumerator = collection.GetEnumerator();
                //_ = enumerator.Current;

            }
        }
    }
}
