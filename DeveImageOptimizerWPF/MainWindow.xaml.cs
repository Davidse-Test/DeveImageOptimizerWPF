using Avalonia;
using Avalonia.Controls;
using PropertyChanged;
using System.Reflection;

namespace DeveImageOptimizerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.axaml
    /// </summary>
    [DoNotNotify]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Title = $"DeveImageOptimizer {Assembly.GetEntryAssembly()?.GetName().Version}";
        }
    }
}
