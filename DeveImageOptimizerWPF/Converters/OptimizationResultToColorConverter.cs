using Avalonia.Data.Converters;
using Avalonia.Media;
using DeveImageOptimizer.State;
using System;
using System.Globalization;

namespace DeveImageOptimizerWPF.Converters
{
    public sealed class OptimizationResultToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Brushes.Red;
            }
            var ofr = (OptimizationResult)value;
            switch (ofr)
            {
                case OptimizationResult.Success:
                    return Brushes.LightGreen;
                case OptimizationResult.Skipped:
                    return Brushes.Yellow;
                case OptimizationResult.InProgress:
                    return Brushes.Blue;
                case OptimizationResult.Failed:
                default:
                    return Brushes.Red;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
