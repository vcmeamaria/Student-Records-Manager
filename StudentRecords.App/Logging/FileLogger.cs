namespace StudentRecords.App.Logging
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;

        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Log(string message)
        {
            string logEntry =
                $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

            File.AppendAllText(
                _filePath,
                logEntry + Environment.NewLine);
        }
    }
}