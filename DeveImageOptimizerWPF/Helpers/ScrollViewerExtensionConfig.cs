using CommunityToolkit.Mvvm.ComponentModel;

namespace DeveImageOptimizerWPF.Helpers
{
    public class ScrollViewerExtensionConfig : ObservableObject
    {
        private bool _alwaysScrollToEnd;
        public bool AlwaysScrollToEnd
        {
            get => _alwaysScrollToEnd;
            set => SetProperty(ref _alwaysScrollToEnd, value);
        }
    }
}