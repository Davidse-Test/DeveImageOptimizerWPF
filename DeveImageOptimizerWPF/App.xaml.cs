using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DeveImageOptimizerWPF.ViewModel;
using DeveImageOptimizerWPF.ViewModel.ObservableData;
using Microsoft.Extensions.DependencyInjection;
using PropertyChanged;
using System;

namespace DeveImageOptimizerWPF
{
    /// <summary>
    /// The Avalonia application for DeveImageOptimizer
    /// </summary>
    [DoNotNotify]
    public partial class App : Application
    {
        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public static new App? Current => Application.Current as App;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        public App()
        {
            Services = ConfigureServices();
            Console.WriteLine("DeveImageOptimizerWPF started");
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            var loggerExtractinator = LoggerExtractinator.CreateLoggerExtractinatorAndSetupConsoleRedirection();
            services.AddSingleton(loggerExtractinator);

            // Viewmodels
            services.AddTransient<MainViewModel>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<ConsoleViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
