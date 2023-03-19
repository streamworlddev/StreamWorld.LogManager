using System.Collections.Generic;
using System.IO;

namespace Log
{
    public class LoggerDictionary<TKey, TValue> : Dictionary<string, Logger>
    {
        public void Add(string TKey)
        {
            base.Add(TKey, new Logger(TKey));
        }
    }
    
    public class LogManager
    {
        public static readonly object lockKey = new object();

        public static LoggerDictionary<string, Logger> Loggers = new LoggerDictionary<string, Logger>();

        static LogManager()
        {
            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");

            Loggers.Add("Main");
        }

    }
}

