using Avalonia;
using Avalonia.Controls;
using System;

namespace DeveImageOptimizerWPF.Helpers
{
    public class ScrollViewerExtensions
    {
        public static readonly AttachedProperty<bool> AlwaysScrollToEndProperty =
            AvaloniaProperty.RegisterAttached<ScrollViewerExtensions, ScrollViewer, bool>("AlwaysScrollToEnd");

        private static bool _autoScroll;

        static ScrollViewerExtensions()
        {
            AlwaysScrollToEndProperty.Changed.AddClassHandler<ScrollViewer>(OnAlwaysScrollToEndChanged);
        }

        private static void OnAlwaysScrollToEndChanged(ScrollViewer scrollViewer, AvaloniaPropertyChangedEventArgs e)
        {
            if (scrollViewer != null)
            {
                bool newValue = (bool)(e.NewValue ?? false);
                bool oldValue = (bool)(e.OldValue ?? false);

                if (newValue)
                {
                    scrollViewer.ScrollToEnd();
                    scrollViewer.PropertyChanged += ScrollViewer_PropertyChanged;
                }
                else
                {
                    scrollViewer.PropertyChanged -= ScrollViewer_PropertyChanged;
                }
            }
            else
            {
                throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.");
            }
        }

        private static void ScrollViewer_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (sender is ScrollViewer scroll && (e.Property.Name == "Extent" || e.Property.Name == "Offset"))
            {
                // Check if we're at the bottom
                _autoScroll = Math.Abs(scroll.Offset.Y - (scroll.Extent.Height - scroll.Viewport.Height)) < 1;
                
                // If we have auto-scroll enabled and content changed, scroll to end
                if (_autoScroll && e.Property.Name == "Extent")
                {
                    scroll.ScrollToEnd();
                }
            }
        }

        public static bool GetAlwaysScrollToEnd(ScrollViewer scrollViewer)
        {
            if (scrollViewer == null) { throw new ArgumentNullException(nameof(scrollViewer)); }
            return scrollViewer.GetValue(AlwaysScrollToEndProperty);
        }

        public static void SetAlwaysScrollToEnd(ScrollViewer scrollViewer, bool value)
        {
            if (scrollViewer == null) { throw new ArgumentNullException(nameof(scrollViewer)); }
            scrollViewer.SetValue(AlwaysScrollToEndProperty, value);
        }
    }
}
