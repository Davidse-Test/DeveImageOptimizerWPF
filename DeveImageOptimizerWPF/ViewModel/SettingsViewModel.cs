using DeveImageOptimizer.ImageOptimization;
using DeveImageOptimizerWPF.Helpers;
using DeveImageOptimizerWPF.State;
using DeveImageOptimizerWPF.State.UserSettings;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Avalonia.Platform.Storage;
using Avalonia.Controls;

namespace DeveImageOptimizerWPF.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class SettingsViewModel : ObservableRecipient
    {
        public UserSettingsData UserSettingsData { get; }

        public IEnumerable<ImageOptimizationLevel> AvailableImageOptimizationLevels { get; }
        public IEnumerable<int> MaxParallelismChoices { get; }
        public IEnumerable<int> AvailableLogLevels { get; }

        public IEnumerable<RemembererSettings> AvailableStorageModes { get; }

        public SettingsViewModel()
        {
            UserSettingsData = StaticState.UserSettingsManager.State;
            BrowseCommandFileOptimizer = new RelayCommand(BrowseCommandFileOptimizerImp);
            BrowseCommandTempDir = new RelayCommand(BrowseCommandTempDirImp);

            SaveCommand = new RelayCommand(SaveCommandImp);
            ResetToDefaultsCommand = new RelayCommand(ResetToDefaultsCommandImpl);

            AvailableImageOptimizationLevels = Enum.GetValues<ImageOptimizationLevel>();
            MaxParallelismChoices = Enumerable.Range(1, Environment.ProcessorCount).ToList();
            AvailableLogLevels = new List<int>() { 0, 1, 2, 3, 4 };

            AvailableStorageModes = Enum.GetValues(typeof(RemembererSettings)).Cast<RemembererSettings>().ToList();
        }

        public ICommand SaveCommand { get; }
        private void SaveCommandImp()
        {
            StaticState.UserSettingsManager.Save();
        }

        public ICommand ResetToDefaultsCommand { get; }
        private void ResetToDefaultsCommandImpl()
        {
            StaticState.UserSettingsManager.State.ResetToDefaults();
        }

        public ICommand BrowseCommandFileOptimizer { get; private set; }
        private async void BrowseCommandFileOptimizerImp()
        {
            var topLevel = TopLevel.GetTopLevel(App.Current?.MainWindow);
            if (topLevel == null) return;

            var fileDialog = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select FileOptimizer executable",
                AllowMultiple = false,
                FileTypeFilter = new[] 
                { 
                    new FilePickerFileType("FileOptimizer") { Patterns = new[] { "FileOptimizer.exe", "FileOptimizer64.exe" } },
                    new FilePickerFileType("All files") { Patterns = new[] { "*.*" } }
                }
            });

            if (fileDialog.Count > 0)
            {
                UserSettingsData.FileOptimizerPath = fileDialog[0].Path.LocalPath;
            }
        }

        public ICommand BrowseCommandTempDir { get; private set; }

        private async void BrowseCommandTempDirImp()
        {
            var topLevel = TopLevel.GetTopLevel(App.Current?.MainWindow);
            if (topLevel == null) return;

            var folderDialog = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                Title = "Select Temp Directory",
                AllowMultiple = false
            });

            if (folderDialog.Count > 0)
            {
                UserSettingsData.TempDirectory = folderDialog[0].Path.LocalPath;
            }
        }
    }
}
