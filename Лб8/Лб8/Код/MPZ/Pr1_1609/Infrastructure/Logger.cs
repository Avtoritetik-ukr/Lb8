using System;
using System.IO;

namespace Pr1_1609.Infrastructure
{
    public static class Logger
    {
        private static string logPath = "log.txt";

        public static void Init(string path)
        {
            logPath = path;
            string? dir = Path.GetDirectoryName(logPath);
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
        }

        public static void Write(string action, string message)
        {
            try
            {
                File.AppendAllText(logPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{action}] {message}{Environment.NewLine}");
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }
    }
}