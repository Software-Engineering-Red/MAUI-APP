using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace UndacApp.Converters
{
    public class BooleanToReservedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool isReserved && isReserved ? "Reserved" : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
