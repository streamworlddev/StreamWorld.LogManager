using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StreamWorld.Log
{

    public class Logger : ILogger
    {
        private readonly string Name;
        List<ICustomLogger> loggerContainer = new List<ICustomLogger>();
    
        public Logger(string name)
        {
           
            Name = name;
        }
        public void AddCustomLogger(ICustomLogger customLogger)
        {
            loggerContainer.Add(customLogger);
        }

        public void Log(string text, params object[] additionalData)
        {
            foreach (var container in loggerContainer)
            {
                container.Log(text,additionalData);
            }
        }
    }
}
