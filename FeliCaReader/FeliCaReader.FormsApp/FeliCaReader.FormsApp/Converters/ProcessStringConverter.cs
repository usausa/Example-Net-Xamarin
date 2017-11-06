namespace FeliCaReader.FormsApp.Converters
{
    using System;
    using System.Globalization;

    using FeliCaReader.FormsApp.Models;

    using Xamarin.Forms;

    public class ProcessStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Suica.ConvertProcessString((byte)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}