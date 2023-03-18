using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

