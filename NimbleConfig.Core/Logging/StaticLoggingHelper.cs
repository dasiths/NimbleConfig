using System;

namespace NimbleConfig.Core.Logging
{
    public static class StaticLoggingHelper
    {
        public static IConfigLogger ConfigLogger { get; set; }

        public static void Trace(string message)
        {
            ConfigLogger?.Trace(message);
        }

        public static void Debug(string message)
        {
            ConfigLogger?.Debug(message);
        }

        public static void Info(string message)
        {
            ConfigLogger?.Info(message);
        }

        public static void Warning(string message)
        {
            ConfigLogger?.Warning(message);
        }

        public static void Error(string message, Exception ex)
        {
            ConfigLogger?.Error(message, ex);
        }
    }
}
