using DeveCoolLib.Streams;
using DeveImageOptimizerWPF.LogViewerData;
using IX.Observable;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DeveImageOptimizerWPF.ViewModel.ObservableData
{
    public class LoggerExtractinator : ObservableObject
    {
        private static object _Lockject = new object();
        private static bool _HasInstance = false;
        private static TextWriter? _OriginalOutTextWriter = null;

        public static LoggerExtractinator CreateLoggerExtractinatorAndSetupConsoleRedirection()
        {
            lock (_Lockject)
            {
                if (_HasInstance)
                {
                    throw new InvalidOperationException($"Cannot create multiple instances of the {nameof(LoggerExtractinator)}");
                }
                _HasInstance = true;
                _OriginalOutTextWriter = Console.Out;
                var originalOut = Console.OpenStandardOutput();
                var consoleOutputStream = new MovingMemoryStream();

                var multiOut = new MultiStream(originalOut, consoleOutputStream);
                var writer = new StreamWriter(multiOut)
                {
                    AutoFlush = true
                };

                Console.SetOut(writer);

                var extractinator = new LoggerExtractinator(consoleOutputStream);
                return extractinator;
            }
        }

        private readonly TextReader _reader;

        private bool _isRunning = false;
        private Task _runningTask;

        private ObservableQueue<string> _logLines = new ObservableQueue<string>();
        public ObservableQueue<string> LogLines
        {
            get => _logLines;
            set => SetProperty(ref _logLines, value);
        }

        private ObservableQueue<LogEntry> _logLinesEntry = new ObservableQueue<LogEntry>();
        public ObservableQueue<LogEntry> LogLinesEntry
        {
            get => _logLinesEntry;
            set => SetProperty(ref _logLinesEntry, value);
        }

        private int lineCount = 0;

        private LoggerExtractinator(MovingMemoryStream movingMemoryStream)
        {
            _reader = new StreamReader(movingMemoryStream);
            _runningTask = Task.Run(Runner);
        }

        public void DestroyAndRevertConsoleOut()
        {
            lock (_Lockject)
            {
                _isRunning = false;
                _runningTask.Wait();
                if (_OriginalOutTextWriter != null)
                {
                    Console.SetOut(_OriginalOutTextWriter);
                }
                _HasInstance = false;
            }
        }

        private void Runner()
        {
            _isRunning = true;
            
            while (_isRunning)
            {
                var logLine = _reader.ReadLine();
                if (logLine != null)
                {
                    try
                    {
                        var toInvoke = new Action(() =>
                        {
                            try
                            {
                                var lineToAdd = $"{lineCount,4}: {logLine}";
                                LogLines.Enqueue(lineToAdd);
                                LogLinesEntry.Enqueue(new LogEntry() { DateTime = DateTime.Now, Index = lineCount, Message = logLine });
                                lineCount++;

                                while (LogLines.Count > 1000)
                                {
                                    LogLines.Dequeue();
                                }
                                while (LogLinesEntry.Count > 1000)
                                {
                                    LogLinesEntry.Dequeue();
                                }
                            }
                            catch (Exception ex)
                            {
                                //Swallow exception as this usually only happens when you kill the application
                            }
                        });
                        
                        Dispatcher.UIThread.Post(toInvoke);
                    }
                    catch (Exception ex)
                    {
                        //Swallow exception as this usually only happens when you kill the application
                    }
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }
    }
}
