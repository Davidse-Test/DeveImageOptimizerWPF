using DeveImageOptimizer.FileProcessing;
using DeveImageOptimizer.ImageOptimization;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace DeveImageOptimizerWPF.State.UserSettings
{
    [Serializable]
    public class UserSettingsData : ObservableObject, IChangable
    {
        private string _fileOptimizerPath;
        public string FileOptimizerPath
        {
            get => _fileOptimizerPath;
            set => SetProperty(ref _fileOptimizerPath, value);
        }

        private string _tempDirectory;
        public string TempDirectory
        {
            get => _tempDirectory;
            set => SetProperty(ref _tempDirectory, value);
        }

        private bool _hideOptimizerWindow;
        public bool HideOptimizerWindow
        {
            get => _hideOptimizerWindow;
            set => SetProperty(ref _hideOptimizerWindow, value);
        }

        private RemembererSettings _remembererSettings;
        public RemembererSettings RemembererSettings
        {
            get => _remembererSettings;
            set => SetProperty(ref _remembererSettings, value);
        }

        private bool _saveFailedFiles;
        public bool SaveFailedFiles
        {
            get => _saveFailedFiles;
            set => SetProperty(ref _saveFailedFiles, value);
        }

        private bool _keepFileAttributes;
        public bool KeepFileAttributes
        {
            get => _keepFileAttributes;
            set => SetProperty(ref _keepFileAttributes, value);
        }

        private bool _executeImageOptimizationParallel;
        public bool ExecuteImageOptimizationParallel
        {
            get => _executeImageOptimizationParallel;
            set => SetProperty(ref _executeImageOptimizationParallel, value);
        }

        private int _maxDegreeOfParallelism;
        public int MaxDegreeOfParallelism
        {
            get => _maxDegreeOfParallelism;
            set => SetProperty(ref _maxDegreeOfParallelism, value);
        }

        private bool _directlyCallOptimizers;
        public bool DirectlyCallOptimizers
        {
            get => _directlyCallOptimizers;
            set => SetProperty(ref _directlyCallOptimizers, value);
        }

        private ImageOptimizationLevel _imageOptimizationLevel;
        public ImageOptimizationLevel ImageOptimizationLevel
        {
            get => _imageOptimizationLevel;
            set => SetProperty(ref _imageOptimizationLevel, value);
        }

        private bool _optimizeJpg;
        public bool OptimizeJpg
        {
            get => _optimizeJpg;
            set => SetProperty(ref _optimizeJpg, value);
        }

        private bool _optimizePng;
        public bool OptimizePng
        {
            get => _optimizePng;
            set => SetProperty(ref _optimizePng, value);
        }

        private bool _optimizeGif;
        public bool OptimizeGif
        {
            get => _optimizeGif;
            set => SetProperty(ref _optimizeGif, value);
        }

        private bool _optimizeBmp;
        public bool OptimizeBmp
        {
            get => _optimizeBmp;
            set => SetProperty(ref _optimizeBmp, value);
        }

        private int _logLevel;
        public int LogLevel
        {
            get => _logLevel;
            set => SetProperty(ref _logLevel, value);
        }

        private bool _isChanged;
        [XmlIgnore]
        public bool IsChanged
        {
            get => _isChanged;
            set => SetProperty(ref _isChanged, value);
        }

        public UserSettingsData()
        {
            ResetToDefaults();
        }

        public void LoadFromConfiguration(DeveImageOptimizerConfiguration config)
        {
            FileOptimizerPath = config.FileOptimizerPath;
            TempDirectory = config.TempDirectory;
            HideOptimizerWindow = config.HideOptimizerWindow;
            SaveFailedFiles = config.SaveFailedFiles;
            KeepFileAttributes = config.KeepFileAttributes;

            ExecuteImageOptimizationParallel = config.ExecuteImageOptimizationParallel;
            MaxDegreeOfParallelism = config.MaxDegreeOfParallelism;

            DirectlyCallOptimizers = config.CallOptimizationToolsDirectlyInsteadOfThroughFileOptimizer;

            ImageOptimizationLevel = config.ImageOptimizationLevel;

            OptimizeJpg = config.OptimizeJpg;
            OptimizePng = config.OptimizePng;
            OptimizeGif = config.OptimizeGif;
            OptimizeBmp = config.OptimizeBmp;

            LogLevel = config.LogLevel;
        }

        public DeveImageOptimizerConfiguration ToDeveImageOptimizerConfiguration()
        {
            var config = new DeveImageOptimizerConfiguration()
            {
                ExecuteImageOptimizationParallel = ExecuteImageOptimizationParallel,
                FileOptimizerPath = FileOptimizerPath,
                HideOptimizerWindow = HideOptimizerWindow,
                LogLevel = LogLevel,
                MaxDegreeOfParallelism = MaxDegreeOfParallelism,
                SaveFailedFiles = SaveFailedFiles,
                KeepFileAttributes = KeepFileAttributes,
                TempDirectory = TempDirectory,
                CallOptimizationToolsDirectlyInsteadOfThroughFileOptimizer = DirectlyCallOptimizers,
                ImageOptimizationLevel = ImageOptimizationLevel,
                OptimizeJpg = OptimizeJpg,
                OptimizePng = OptimizePng,
                OptimizeGif = OptimizeGif,
                OptimizeBmp = OptimizeBmp
            };
            return config;
        }

        public void ResetToDefaults()
        {
            RemembererSettings = RemembererSettings.StorePerFile;
            LoadFromConfiguration(new DeveImageOptimizerConfiguration());
        }
    }
}
