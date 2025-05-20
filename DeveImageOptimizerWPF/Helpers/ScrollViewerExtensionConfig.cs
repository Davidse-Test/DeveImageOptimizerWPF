using PropertyChanged;

namespace DeveImageOptimizerWPF.Helpers
{
    [AddINotifyPropertyChangedInterface]
    public class ScrollViewerExtensionConfig
    {
        public bool AlwaysScrollToEnd { get; set; }
    }
}