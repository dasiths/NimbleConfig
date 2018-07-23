using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NimbleConfig.Core.Logging;

namespace NimbleConfig.Sample
{
    public class CustomConfigConfigLogger: IConfigLogger
    {
        private readonly ILogger<CustomConfigConfigLogger> _logger;

        public CustomConfigConfigLogger(ILogger<CustomConfigConfigLogger> logger)
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
