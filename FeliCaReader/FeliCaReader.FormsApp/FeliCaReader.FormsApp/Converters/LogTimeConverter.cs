namespace FeliCaReader.FormsApp.Converters
{
    using System;
    using System.Globalization;

    using FeliCaReader.FormsApp.Models;

    using Xamarin.Forms;

    public class LogTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var log = (SuicaLogData)value;
            return Suica.IsProcessOfSales(log.Process) ? log.DateTime : (DateTime?)null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}