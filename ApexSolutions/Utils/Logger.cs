using System;
using System.IO;

namespace ApexCare.Utils
{
    public class Logger
    {
        private readonly string _logFilePath;

        // Constructor
        public Logger(string logFilePath)
        {
            _logFilePath = logFilePath;

            // Ensure the log file exists
            if (!File.Exists(_logFilePath))
            {
                File.Create(_logFilePath).Dispose(); // Create file if it doesn't exist
            }
        }

        // Log an info message
        public void LogInfo(string message)
        {
            Log("INFO", message);
        }

        // Log a warning message
        public void LogWarning(string message)
        {
            Log("WARNING", message);
        }

        // Log an error message
        public void LogError(string message)
        {
            Log("ERROR", message);
        }

        // Internal method to handle the actual logging
        private void Log(string level, string message)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
            Console.WriteLine(logEntry);

            // Append log entry to the file
            using (var writer = new StreamWriter(_logFilePath, true))
            {
                writer.WriteLine(logEntry);
            }
        }
    }
}
