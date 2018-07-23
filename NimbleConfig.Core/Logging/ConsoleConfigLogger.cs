using System;

namespace NimbleConfig.Core.Logging
{
    public class ConsoleConfigLogger : IConfigLogger
    {
        private static readonly Func<string, string, string> Template = 
            (string type, string message) => $"[{nameof(NimbleConfig)}] | [{type}]: {message}"; 

        public void Trace(string message)
        {
            Console.WriteLine(Template("Trace", message));
        }

        public void Debug(string message)
        {
            Console.WriteLine(Template("Debug", message));
        }

        public void Info(string message)
        {
            Console.WriteLine(Template("Info", message));
        }

        public void Warning(string message)
        {
            Console.WriteLine(Template("Warning !!!", message));
        }

        public void Error(string message, Exception ex)
        {
            Console.Error.WriteLine(Template("Exception", message));
            Console.Error.WriteLine(Template("Exception Details", ex.Message));
            Console.Error.WriteLine(Template("Exception Stack Trace", ex.StackTrace));
        }
    }
}