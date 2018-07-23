using System;

namespace NimbleConfig.Core.Logging
{
    public interface IConfigLogger
    {
        void Trace(string message);
        void Debug(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message, Exception ex);
    }
}
