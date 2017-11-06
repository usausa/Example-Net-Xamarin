namespace FeliCaReader.FormsApp.Converters
{
    using System;
    using System.Globalization;

    using FeliCaReader.FormsApp.Models;

    using Xamarin.Forms;

    public class ProcessColor
    {
        public int ProcessType { get; set; }

        public Color Color { get; set; }
    }

    public class ProcessColorConverter : IValueConverter
    {
        public ProcessColor[] Colors { get; set; }

        public Color DefaultColor { get; set; } = Color.Gray;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var processType = (int)Suica.ConvertProcessType((byte)value);

            foreach (var color in Colors)
            {
                if (color.ProcessType == processType)
                {
                    return color.Color;
                }
            }

            return DefaultColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}