using DeveImageOptimizerWPF.Helpers;
using DeveImageOptimizerWPF.ViewModel.ObservableData;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace DeveImageOptimizerWPF.ViewModel
{
    public class ConsoleViewModel : ObservableObject
    {
        private LoggerExtractinator _loggerExtractinator;
        public LoggerExtractinator LoggerExtractinator
        {
            get => _loggerExtractinator;
            set => SetProperty(ref _loggerExtractinator, value);
        }

        private int _consoleFontSize = 12;
        public int ConsoleFontSize
        {
            get => _consoleFontSize;
            set => SetProperty(ref _consoleFontSize, value);
        }

        public ICommand IncreaseFontSizeCommand { get; }
        public ICommand DecreaseFontSizeCommand { get; }

        private ScrollViewerExtensionConfig _scrollConfig = new ScrollViewerExtensionConfig() { AlwaysScrollToEnd = true };
        public ScrollViewerExtensionConfig ScrollConfig
        {
            get => _scrollConfig;
            set => SetProperty(ref _scrollConfig, value);
        }

        public ConsoleViewModel(LoggerExtractinator loggerExtractinator)
        {
            LoggerExtractinator = loggerExtractinator;

            IncreaseFontSizeCommand = new RelayCommand(() => ConsoleFontSize = Math.Clamp(ConsoleFontSize + 2, 12, 30));
            DecreaseFontSizeCommand = new RelayCommand(() => ConsoleFontSize = Math.Clamp(ConsoleFontSize - 2, 12, 30));
        }
    }
}
