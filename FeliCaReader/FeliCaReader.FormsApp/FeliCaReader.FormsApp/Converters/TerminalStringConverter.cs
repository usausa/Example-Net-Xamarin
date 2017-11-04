namespace FeliCaReader.FormsApp.Converters
{
    using System;
    using System.Globalization;

    using FeliCaReader.FormsApp.Models;

    using Xamarin.Forms;

    public class TerminalStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Suica.ConvertTerminalString((byte)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}