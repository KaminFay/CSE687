using System;
using Windows.UI.Xaml.Data;

namespace guiApp
{
    public class TextFormatter : IValueConverter
    {
        public String stringFormat { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!String.IsNullOrEmpty(stringFormat))
            {
                return String.Format(stringFormat, value);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
