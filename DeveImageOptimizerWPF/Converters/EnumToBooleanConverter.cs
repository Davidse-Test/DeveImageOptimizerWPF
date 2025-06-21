using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace DeveImageOptimizerWPF.Converters
{
    public class EnumToBooleanConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null && parameter == null)
            {
                return true;
            }
            else if (value == null)
            {
                return false;
            }
            return value.Equals(parameter);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue && boolValue && parameter != null)
            {
                return parameter;
            }
            return null;
        }
    }
}
