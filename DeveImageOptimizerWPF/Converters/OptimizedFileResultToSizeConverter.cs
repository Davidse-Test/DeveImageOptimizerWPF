using Avalonia.Data.Converters;
using DeveCoolLib.Conversion;
using DeveImageOptimizerWPF.State.ProcessingState;
using System;
using System.Globalization;

namespace DeveImageOptimizerWPF.Converters
{
    public sealed class OptimizedFileResultToSizeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }
            var ofr = (OptimizableFileUI)value;
            return ValuesToStringHelper.BytesToString(ofr.OriginalSize - ofr.OptimizedSize, culture);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
