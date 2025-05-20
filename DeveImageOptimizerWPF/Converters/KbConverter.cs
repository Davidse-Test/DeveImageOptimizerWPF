using Avalonia.Data.Converters;
using DeveCoolLib.Conversion;
using System;
using System.Globalization;

namespace DeveImageOptimizerWPF.Converters
{
    public sealed class KbConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is long longValue)
            {
                return ValuesToStringHelper.BytesToString(longValue, culture);
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
