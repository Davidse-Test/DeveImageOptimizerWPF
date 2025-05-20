using Avalonia;
using Avalonia.Controls;
using PropertyChanged;
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
            AlwaysScrollToEndProperty.Changed.AddClassHandler<ScrollViewer>((s, e) => OnAlwaysScrollToEndChanged(s, e));
        }

        private static void OnAlwaysScrollToEndChanged(ScrollViewer scrollViewer, AvaloniaPropertyChangedEventArgs e)
        {
            if (scrollViewer != null)
            {
                bool newValue = (bool)e.NewValue!;
                bool oldValue = (bool)e.OldValue!;

                if (newValue)
                {
                    scrollViewer.ScrollToEnd();
                    scrollViewer.ScrollChanged += ScrollChanged;
                }
                else
                {
                    scrollViewer.ScrollChanged -= ScrollChanged;
                }
            }
            else
            {
                throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.");
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

        private static void ScrollChanged(object? sender, ScrollChangedEventArgs e)
        {
            if (sender is ScrollViewer scroll)
            {
                // User scroll event : set or unset autoscroll mode
                if (e.ExtentHeightChange == 0)
                {
                    _autoScroll = scroll.Offset.Y == scroll.ScrollBarMaximum.Y;
                    scroll.SetValue(AlwaysScrollToEndProperty, _autoScroll);
                }

                // Content scroll event : autoscroll eventually
                if (_autoScroll && e.ExtentHeightChange != 0)
                {
                    scroll.ScrollToEnd();
                }
            }
            else
            {
                throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.");
            }
        }
    }
}
