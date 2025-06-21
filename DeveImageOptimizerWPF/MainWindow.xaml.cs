using Avalonia;
using Avalonia.Controls;
using System.Reflection;

namespace DeveImageOptimizerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.axaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Title = $"DeveImageOptimizer {Assembly.GetEntryAssembly()?.GetName().Version}";
        }
    }
}
