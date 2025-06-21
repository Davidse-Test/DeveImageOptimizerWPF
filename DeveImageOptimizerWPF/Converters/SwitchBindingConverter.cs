using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace DeveImageOptimizerWPF.Converters
{
    public class SwitchBindingConverter : IValueConverter
    {
        public static SwitchBindingConverter Instance { get; } = new SwitchBindingConverter();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue && parameter is string options)
            {
                string[] parts = options.Split(';');
                if (parts.Length == 2)
                {
                    return boolValue ? parts[1] : parts[0];
                }
            }
            return value;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}