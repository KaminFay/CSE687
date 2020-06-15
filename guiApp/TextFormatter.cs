/*
 * TextFormatter.cs - Group of functions that are used to correctly format text
 * within the GUI.
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
using Windows.UI.Xaml.Data;

namespace guiApp
{
    public class TextFormatter : IValueConverter
    {
        public String stringFormat { get; set; }

        /*
         * ----< Function > Convert
         * ----< Description > 
         * Converts an incoming string from MainPage xaml to be converted
         * and displays the correct pattern in the GUI.
         * ----< Description >
         * @Param None
         * @Return None
         */
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!String.IsNullOrEmpty(stringFormat))
            {
                return String.Format(stringFormat, value);
            }

            return value;
        }

        /*
         * ----< Function > ConvertBack
         * ----< Description > 
         * Unused but required.
         * ----< Description >
         * @Param None
         * @Return None
         */
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
