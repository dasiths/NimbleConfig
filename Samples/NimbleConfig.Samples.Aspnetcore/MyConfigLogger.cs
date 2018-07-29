using System;
using Microsoft.Extensions.Logging;
using NimbleConfig.Core.Logging;

namespace NimbleConfig.Samples.Aspnetcore
{
    public class MyConfigLogger: IConfigLogger
    {
        private readonly ILogger<MyConfigLogger> _logger;

        public MyConfigLogger(ILogger<MyConfigLogger> logger)
        {
            _logger = logger;
        }

        public void Trace(string message)
        {
            _logger.LogTrace(message);
        }

        public void Debug(string message)
        {
            _logger.LogDebug(message);
        }

        public void Info(string message)
        {
            _logger.LogInformation(message);
        }

        public void Warning(string message)
        {
            _logger.LogWarning(message);
        }

        public void Error(string message, Exception ex)
        {
            _logger.LogError(ex, message);
        }
    }
}
