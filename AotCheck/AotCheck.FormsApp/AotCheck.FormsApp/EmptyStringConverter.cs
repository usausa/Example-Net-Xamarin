namespace AotCheck.FormsApp
{
    using System;
    using System.Globalization;

    using Xamarin.Forms;

    public class EmptyStringConverter : IValueConverter
    {
        public string EmptyText { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.IsNullOrEmpty(value as string) ? EmptyText : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
