using System;
using System.IO;

namespace StreamWorld.Log
{
    public class TextFileLogger : ICustomLogger
    {
        string Name;
        string FileDirectory = "Logs";

        public TextFileLogger(string id) 
        {
            Name = id;
        }
        public TextFileLogger(string id,string directory)
        {
            Name = id;
            FileDirectory = directory;
            CheckLogDirectory();
        }

        public void Log(string text, params object[] additionalData)
        {
            WriteToTextFile(text);
        }

        public void WriteToTextFile(string text)
        {
            lock (LogManager.lockKey)
            {
                string filePath = $"{FileDirectory}/{Name}_{DateTime.Now.ToString("dd.MM.yyyy").Replace("/", "_").Replace(@"\", "_").Replace(".", "_")}.txt";
                using (var file = new StreamWriter(filePath, true))
                {
                    file.WriteLine($"[{DateTime.Now:G}] {text}");
                }
            }
        }

        private void CheckLogDirectory()
        {
            if (!Directory.Exists(FileDirectory))
            {
                Directory.CreateDirectory(FileDirectory);
            }
        }
    }

}
