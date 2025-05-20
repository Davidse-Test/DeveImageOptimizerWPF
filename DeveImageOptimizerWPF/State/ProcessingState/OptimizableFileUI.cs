using DeveCoolLib.Collections;
using DeveImageOptimizer.State;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace DeveImageOptimizerWPF.State.ProcessingState
{
    public class OptimizableFileUI : ObservableObject
    {
        private string _path;
        public string Path
        {
            get => _path;
            set => SetProperty(ref _path, value);
        }

        private string _relativePath;
        public string RelativePath
        {
            get => _relativePath;
            set => SetProperty(ref _relativePath, value);
        }

        private OptimizationResult _optimizationResult = OptimizationResult.InProgress;
        public OptimizationResult OptimizationResult
        {
            get => _optimizationResult;
            set => SetProperty(ref _optimizationResult, value);
        }

        private long _originalSize;
        public long OriginalSize
        {
            get => _originalSize;
            set => SetProperty(ref _originalSize, value);
        }

        private long _optimizedSize;
        public long OptimizedSize
        {
            get => _optimizedSize;
            set => SetProperty(ref _optimizedSize, value);
        }

        private TimeSpan _duration = TimeSpan.Zero;
        public TimeSpan Duration
        {
            get => _duration;
            set => SetProperty(ref _duration, value);
        }

        public ObservableCollection<string> Errors { get; } = new ObservableCollection<string>();

        public OptimizableFileUI(OptimizableFile optimizableFile)
        {
            Set(optimizableFile);
        }

        public void Set(OptimizableFile optimizableFile)
        {
            Path = optimizableFile.Path;
            RelativePath = optimizableFile.RelativePath;

            OptimizationResult = optimizableFile.OptimizationResult;

            OriginalSize = optimizableFile.OriginalSize;
            OptimizedSize = optimizableFile.OptimizedSize;

            Duration = optimizableFile.Duration;

            ListSynchronizerV2.SynchronizeLists(optimizableFile.Errors, Errors);
        }
    }
}
