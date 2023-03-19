using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Log
{
    public class RichTextBoxLogger : IColorableLogger
    {
        private RichTextBox richTextBox;
        public RichTextBox RichTextBox
        {
            get { return richTextBox; }
            set { richTextBox = value; }
        }

        public RichTextBoxLogger(RichTextBox _richTextBox)
        {
            RichTextBox = _richTextBox;
        }

        public void Log(string text, Color color)
        {
            if (richTextBox != null)
            {
                WriteToRichTextBox($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] ", Color.Red);
                WriteToRichTextBox(text + "\n", color);
            }
        }

        public void WriteToRichTextBox(string text, Color color)
        {

            ControlSystem.SafeInvoke(richTextBox, (MethodInvoker)(() => {
                richTextBox.SelectionStart = richTextBox.TextLength;
                richTextBox.ScrollToCaret();
                richTextBox.SelectionLength = 0;
                richTextBox.SelectionColor = color;
                richTextBox.AppendText(text);
                richTextBox.SelectionColor = richTextBox.ForeColor;
            }));
        }


        public void Clear()
        {
            richTextBox.Invoke(new Action(delegate ()
            {
                richTextBox.Clear();
            }));
        }

    }

    public class TextFileLogger : ILogger
    {

        string Name;
        public TextFileLogger(string name)
        {
            Name = name;
        }

        public void Log(string text)
        {
            WriteToTextFile(text);
        }

        public void WriteToTextFile(string text)
        {
            lock (LogManager.lockKey)
            {
                string filePath = $"Logs/{Name}_{DateTime.Now.ToString("dd.MM.yyyy").Replace("/", "_").Replace(@"\", "_").Replace(".", "_")}.txt";
                using (var file = new StreamWriter(filePath, true))
                {
                    file.WriteLine($"[{DateTime.Now:G}] {text}");
                }
            }

        }
    }

    public class Logger
    {
        private TextFileLogger textFileLogger;
        public RichTextBoxLogger richTextBoxLogger; 

        public Logger(string name, RichTextBox richTextBox = null)
        {
            textFileLogger = new TextFileLogger(name);
            richTextBoxLogger = new RichTextBoxLogger(richTextBox);

        }

        public void Log(string text) => textFileLogger.Log(text);

        public void Log(string text, Color color)
        {
            richTextBoxLogger.Log(text, color);
            textFileLogger.Log(text);
        }
    }
}
