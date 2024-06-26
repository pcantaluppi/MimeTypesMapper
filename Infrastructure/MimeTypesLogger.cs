using Microsoft.Extensions.Logging;

namespace MimeTypesMapper.Infrastructure
{
    public static class MimeTypesLogger
    {
        private static ILoggerFactory? _loggerFactory;
        private static ILogger? _logger;

        public static void ConfigureLogger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger("MimeTypesLogger");
        }

        public static void Log(string message)
        {
            _logger?.LogInformation(message);
        }

        public static void LogError(string message)
        {
            _logger?.LogError(message);
        }
    }
}
