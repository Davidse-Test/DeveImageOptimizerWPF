using DeveImageOptimizer.Exceptions;
using DeveImageOptimizer.FileProcessing;
using DeveImageOptimizer.State;
using DeveImageOptimizer.State.StoringProcessedDirectories;
using DeveImageOptimizerWPF.Helpers;
using DeveImageOptimizerWPF.State;
using DeveImageOptimizerWPF.State.MainWindowState;
using DeveImageOptimizerWPF.State.UserSettings;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using Avalonia.Controls;
using System.Windows.Input;
using MsBox.Avalonia;
using Avalonia;

namespace DeveImageOptimizerWPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ObservableRecipient
    {
        public State.MainWindowState.WindowState WindowState { get; set; }
        public FileProgressState FilesProcessingState { get; set; }

        public bool PreviewEnabled { get; set; }
        public bool IsOptimizing { get; set; }

        private readonly FileProcessedStateRememberer _fileRememberer;
        private readonly DirProcessedStateRememberer _dirRememberer;

        public MainViewModel()
        {
            WindowState = StaticState.WindowStateManager.State;
            FilesProcessingState = new FileProgressState();

            WindowState.PropertyChanged += ProcessingStateData_PropertyChanged;
            FilesProcessingState.PropertyChanged += FilesProcessingState_PropertyChanged;

            GoCommand = new AsyncRelayCommand(GoCommandImp);
            BrowseCommand = new RelayCommand(BrowseCommandImp);

            var optimize = GetRemembererSettings();

            _fileRememberer = new FileProcessedStateRememberer(optimize.fileOptimize);
            _dirRememberer = new DirProcessedStateRememberer(optimize.dirOptimize);

            StaticState.UserSettingsManager.State.PropertyChanged += State_PropertyChanged;
        }

        private (bool fileOptimize, bool dirOptimize) GetRemembererSettings()
        {
            var state = StaticState.UserSettingsManager.State;

            var fileOptimize = state.RemembererSettings == RemembererSettings.OptimizeAlways || state.RemembererSettings == RemembererSettings.StorePerDirectory;
            var dirOptimize = state.RemembererSettings == RemembererSettings.OptimizeAlways || state.RemembererSettings == RemembererSettings.StorePerFile;

            return (fileOptimize, dirOptimize);
        }

        private void State_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var optimize = GetRemembererSettings();

            _fileRememberer.ShouldAlwaysOptimize = optimize.fileOptimize;
            _dirRememberer.ShouldAlwaysOptimize = optimize.dirOptimize;
        }

        private void FilesProcessingState_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        private void ProcessingStateData_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StaticState.WindowStateManager.Save();
        }

        public ICommand GoCommand { get; private set; }
        private async Task GoCommandImp()
        {
            IsOptimizing = true;
            try
            {
                var state = StaticState.UserSettingsManager.State;

                var config = state.ToDeveImageOptimizerConfiguration();

                var fileProcessor = new DeveImageOptimizerProcessor(config, FilesProcessingState, _fileRememberer, _dirRememberer);

                await fileProcessor.ProcessDirectory(WindowState.ProcessingDirectory);
            }
            catch (FileOptimizerNotFoundException ex)
            {
                ShowFileOptimizerNotFoundError(ex.Message);
            }
            catch (AggregateException ex) when (ex.InnerExceptions?.OfType<FileOptimizerNotFoundException>()?.Any() == true)
            {
                var message = ex.InnerExceptions?.OfType<FileOptimizerNotFoundException>().FirstOrDefault()?.Message;
                ShowFileOptimizerNotFoundError(message);
            }
            finally
            {
                IsOptimizing = false;
            }
        }

        private static async void ShowFileOptimizerNotFoundError(string? message)
        {
            var messageBox = MessageBoxManager.GetMessageBoxStandard(
                "Could not find FileOptimizer.exe",
                message ?? "FileOptimizer.exe could not be found",
                icon: MsBox.Avalonia.Enums.Icon.Error);

            await messageBox.ShowAsync();
        }

        public ICommand BrowseCommand { get; private set; }

        private async void BrowseCommandImp()
        {
            ////This can also be applied for SaveFilePicker.
            //var files = await _target.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
            //{
            //    Title = title,
            //    //You can add either custom or from the built-in file types. See "Defining custom file types" on how to create a custom one.
            //    FileTypeFilter = new[] { ImageAll, FilePickerFileTypes.TextPlain }
            //});


            //// Get current window from Avalonia Application.Current
            //var topLevel = TopLevel.GetTopLevel(Application.Current?.MainWindow);
            //if (topLevel == null) return;

            //var folderDialog = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            //{
            //    Title = "Select Folder",
            //    AllowMultiple = false
            //});

            //if (folderDialog.Count > 0)
            //{
            //    WindowState.ProcessingDirectory = folderDialog[0].Path.LocalPath;
            //}
        }
    }
}