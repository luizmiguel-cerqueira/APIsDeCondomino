using System;
using System.IO;
using System.Text;

namespace APIsDeCondomino.model 
{
    public static class Logger
    {
        private static readonly object _lock = new object();
        private static readonly string _logDirectory = "Logs";

        static Logger()
        {
            if (!Directory.Exists(_logDirectory))
                Directory.CreateDirectory(_logDirectory);
        }

        private static string GetLogFilePath()
        {
            string fileName = $"log_{DateTime.Now:yyyyMMdd}.txt";
            return Path.Combine(_logDirectory, fileName);
        }

        private static void WriteLog(string level, string message)
        {
            lock (_lock)
            {
                string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level.ToUpper()}] {message}";
                File.AppendAllText(GetLogFilePath(), logLine + Environment.NewLine, Encoding.UTF8);
            }
        }

        public static void Info(string message) => WriteLog("INFO", message);
        public static void Warn(string message) => WriteLog("WARN", message);
        public static void Error(string message) => WriteLog("ERROR", message);
    }

}