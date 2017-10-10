namespace Inventory.Client.Converters
{
    using System;
    using System.Globalization;

    using Inventory.Client.Models.Entity;

    using Xamarin.Forms;

    public class InspectStatusColorConverter : IValueConverter
    {
        public Color InspectedColor { get; set; }

        public Color CheckedColor { get; set; }

        public Color UnCheckedColor { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var entity = (InspectionStatusEntity)value;
            if (entity.IsChecked)
            {
                return CheckedColor;
            }

            if (entity.InspectionUserId.HasValue)
            {
                return InspectedColor;
            }

            return UnCheckedColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
