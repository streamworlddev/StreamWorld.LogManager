using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace StreamWorld.Log
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
            Loggers.Add("Main");

        }
    }
}

