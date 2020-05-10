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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace guiApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<DLLObject> collection = new ObservableCollection<DLLObject>();

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
                    output.Append(file.Name + "\n");
                }
                //OutputTextBlock.Text = output.ToString();
            }
            else
            {
                //OutputTextBlock.Text = "Operation cancelled.";
            }

        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
                     
            for (int i = 0; i < 5; i ++)
            {
                
                collection.Add(new DLLObject($"item {i}"));
            }

            this.Items.ItemsSource = collection;
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

            //return temp;

        }

        private void ListView_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}