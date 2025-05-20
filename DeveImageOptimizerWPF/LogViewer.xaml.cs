using DeveImageOptimizerWPF.LogViewerData;
using IX.Observable;
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DeveImageOptimizerWPF
{
    /// <summary>
    /// Interaction logic for LogViewer.axaml
    /// </summary>
    public partial class LogViewer : UserControl
    {
        public static readonly StyledProperty<ObservableQueue<LogEntry>> LogLinesProperty = AvaloniaProperty.Register<LogViewer, ObservableQueue<LogEntry>>(
            "LogLines",
            new ObservableQueue<LogEntry>(new List<LogEntry>() { new LogEntry() { DateTime = DateTime.Now, Index = 0, Message = "Test" } }));

        public ObservableQueue<LogEntry> LogLines
        {
            get => GetValue(LogLinesProperty);
            set => SetValue(LogLinesProperty, value);
        }

        public static readonly StyledProperty<int> LogViewerFontSizeProperty = AvaloniaProperty.Register<LogViewer, int>(
            "LogViewerFontSize", 
            12);

        public int LogViewerFontSize
        {
            get => GetValue(LogViewerFontSizeProperty);
            set => SetValue(LogViewerFontSizeProperty, value);
        }

        public LogViewer()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
