using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinSoundMixer.App.Helpers
{
    public static class LogHelper
    {
        private static readonly object lockObj = new object();
        private static readonly string logFilePath;

        static LogHelper()
        {
            string appName = "WinSoundMixer";
            string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), appName);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            logFilePath = Path.Combine(logDirectory, "WinSoundMixer.Log.txt");
        }
        public static string GetLogFilePath()
        {
            return logFilePath; // Daha önce tanımladığınız logFilePath
        }
        public static void Log(string message ,LogLevel logLevel = LogLevel.Info) {
            lock (lockObj)
            {
                try
                {
                    string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] - [{Thread.CurrentThread.ManagedThreadId}] - [{logLevel}] - {message}";
                    File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
                }
                catch (Exception e)
                {
                    File.AppendAllText(logFilePath, $"Error logging message: {e.Message}" + Environment.NewLine);
                }
            }
        }
    }

    public enum LogLevel
    {
        Info,
        Warn,
        Error,
        Debug
    }
}
