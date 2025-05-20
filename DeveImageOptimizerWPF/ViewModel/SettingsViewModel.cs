using DeveImageOptimizer.ImageOptimization;
using DeveImageOptimizerWPF.Helpers;
using DeveImageOptimizerWPF.State;
using DeveImageOptimizerWPF.State.UserSettings;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Avalonia.Platform.Storage;
using Avalonia.Controls;

namespace DeveImageOptimizerWPF.ViewModel
{
    public class SettingsViewModel : ObservableObject
    {
        private UserSettingsData _userSettingsData;
        public UserSettingsData UserSettingsData
        {
            get => _userSettingsData;
            set => SetProperty(ref _userSettingsData, value);
        }

        private IEnumerable<ImageOptimizationLevel> _availableImageOptimizationLevels;
        public IEnumerable<ImageOptimizationLevel> AvailableImageOptimizationLevels
        {
            get => _availableImageOptimizationLevels;
            set => SetProperty(ref _availableImageOptimizationLevels, value);
        }

        private IEnumerable<int> _maxParallelismChoices;
        public IEnumerable<int> MaxParallelismChoices
        {
            get => _maxParallelismChoices;
            set => SetProperty(ref _maxParallelismChoices, value);
        }

        private IEnumerable<int> _availableLogLevels;
        public IEnumerable<int> AvailableLogLevels
        {
            get => _availableLogLevels;
            set => SetProperty(ref _availableLogLevels, value);
        }

        private IEnumerable<RemembererSettings> _availableStorageModes;
        public IEnumerable<RemembererSettings> AvailableStorageModes
        {
            get => _availableStorageModes;
            set => SetProperty(ref _availableStorageModes, value);
        }

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
            //var topLevel = TopLevel.GetTopLevel(Application.Current?.MainWindow);
            //if (topLevel == null) return;

            //var fileDialog = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            //{
            //    Title = "Select FileOptimizer executable",
            //    AllowMultiple = false,
            //    FileTypeFilter = new[] 
            //    { 
            //        new FilePickerFileType("FileOptimizer") { Patterns = new[] { "FileOptimizer.exe", "FileOptimizer64.exe" } },
            //        new FilePickerFileType("All files") { Patterns = new[] { "*.*" } }
            //    }
            //});

            //if (fileDialog.Count > 0)
            //{
            //    UserSettingsData.FileOptimizerPath = fileDialog[0].Path.LocalPath;
            //}
        }

        public ICommand BrowseCommandTempDir { get; private set; }

        private async void BrowseCommandTempDirImp()
        {
            //var topLevel = TopLevel.GetTopLevel(Application.Current?.MainWindow);
            //if (topLevel == null) return;

            //var folderDialog = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            //{
            //    Title = "Select Temp Directory",
            //    AllowMultiple = false
            //});

            //if (folderDialog.Count > 0)
            //{
            //    UserSettingsData.TempDirectory = folderDialog[0].Path.LocalPath;
            //}
        }
    }
}
