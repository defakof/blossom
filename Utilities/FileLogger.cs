namespace blossom.Utilities
{
    public static class FileLogger
    {
        private static string logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "blossom_log.txt");
        private static readonly TimeSpan sessionTimeout = TimeSpan.FromMinutes(5);
        private static DateTime lastLogTime = DateTime.MinValue;
        private static List<string> recentLogs = new();
        private static readonly int maxRecentLogs = 100;
        public static bool DebugMode { get; set; } = false;

        public enum LogCategory
        {
            UI,
            Backend,
            FileOperation,
            Navigation,
            General,
            Error
        }

        static FileLogger()
        {
            EnsureLogFileExists();
        }

        private static void EnsureLogFileExists()
        {
            if (!File.Exists(logFile))
            {
                using (File.Create(logFile)) { }
            }
        }

        public static void Log(string message, LogCategory category = LogCategory.General)
        {
            try
            {
                CheckAndLogNewSession();
                string logMessage = $"{DateTime.Now}: [{category}] {message}";

                if (DebugMode || category == LogCategory.Error)
                {
                    File.AppendAllText(logFile, logMessage + Environment.NewLine);
                    lastLogTime = DateTime.Now;
                }
                else
                {
                    AddToRecentLogs(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

        public static void LogException(string context, Exception ex, LogCategory category = LogCategory.Error)
        {
            Log($"Exception in {context}: {ex.Message}", category);
            Log($"Stack Trace: {ex.StackTrace}", category);
        }

        private static void CheckAndLogNewSession()
        {
            if (DateTime.Now - lastLogTime <= sessionTimeout) return;
            string sessionSeparator = $"{Environment.NewLine}--- New Session Started at {DateTime.Now} ---{Environment.NewLine}";
            File.AppendAllText(logFile, sessionSeparator);
        }

        private static void AddToRecentLogs(string log)
        {
            if (recentLogs.Contains(log)) return;
            recentLogs.Add(log);
            if (recentLogs.Count > maxRecentLogs)
            {
                recentLogs.RemoveAt(0);
            }
        }

        public static void FlushLogs()
        {
            try
            {
                CheckAndLogNewSession();
                File.AppendAllLines(logFile, recentLogs);
                recentLogs.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error flushing logs: {ex.Message}");
            }
        }
    }
}