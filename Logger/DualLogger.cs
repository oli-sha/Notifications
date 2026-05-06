using System;
using System.IO;
using Notifications.Interfaces;

namespace Notifications.Logger
{
    public class DualLogger : ILogger
    {
        private readonly string _logPath;
        public event Action<string>? OnNewLog;

        public DualLogger()
        {
            _logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app_log.txt");
            var header = $"[START] {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            File.WriteAllText(_logPath, header + Environment.NewLine);
            OnNewLog?.Invoke(header);
        }

        private void WriteToFile(string text)
        {
            try
            {
                File.AppendAllText(_logPath, text + Environment.NewLine);
            }
            catch (Exception ex)
            {
                OnNewLog?.Invoke($"[FATAL] Ошибка записи в файл: {ex.Message}");
            }
        }

        public void LogInfo(string message)
        {
            var entry = $"[INFO] {DateTime.Now:HH:mm:ss} - {message}";
            WriteToFile(entry);
            OnNewLog?.Invoke(entry);
        }

        public void LogError(string message)
        {
            var entry = $"[ERROR] {DateTime.Now:HH:mm:ss} - {message}";
            WriteToFile(entry);
            OnNewLog?.Invoke(entry);
        }

        public void LogWarning(string message)
        {
            var entry = $"[WARN] {DateTime.Now:HH:mm:ss} - {message}";
            WriteToFile(entry);
            OnNewLog?.Invoke(entry);
        }
    }
}