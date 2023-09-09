using System;
using System.Drawing;
using System.Windows.Forms;

namespace StreamWorld.LogManager
{
    public class RichTextBoxLogger : ICustomLogger
    {
        private RichTextBox richTextBox;

        /// <summary>
        /// Default cleaning interval is 1 day.
        /// </summary>
        public TimeSpan? CleaningInterval = new TimeSpan(1,0, 0, 0);
        public RichTextBox RichTextBox
        {
            get { return richTextBox; }
            set { richTextBox = value; }
        }

        public Color defaultTextColor = Color.White;

        public DateTime startedDateTime = DateTime.MinValue;


        public RichTextBoxLogger(RichTextBox _richTextBox,TimeSpan? _cleaningInterval = null)
        {
            RichTextBox = _richTextBox;
            if (_cleaningInterval != null) CleaningInterval = _cleaningInterval;
            startedDateTime = DateTime.Now;

        }

        public void Log(string text, params object[] additionalData)
        {
            if (richTextBox != null)
            {
                if (DateTime.Now  > (startedDateTime + CleaningInterval))
                {
                    Clear();
                    startedDateTime = DateTime.Now;
                }

                WriteToRichTextBox($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] ", Color.Red);
                WriteToRichTextBox(text + "\n", (additionalData.Length > 0) ? ( (additionalData[0] is Color) ? (Color)additionalData[0] : defaultTextColor ) : defaultTextColor);
            }
        }

        public void WriteToRichTextBox(string text, Color color)
        {
            richTextBox.Invoke(new Action(delegate ()
            {
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
}
