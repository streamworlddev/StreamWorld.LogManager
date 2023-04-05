using System;
using System.Drawing;
using System.Windows.Forms;

namespace StreamWorld.Log
{
    public class RichTextBoxLogger : ICustomLogger
    {
        private RichTextBox richTextBox;
        public RichTextBox RichTextBox
        {
            get { return richTextBox; }
            set { richTextBox = value; }
        }

        public Color defaultTextColor = Color.White;

        public RichTextBoxLogger(RichTextBox _richTextBox)
        {
            RichTextBox = _richTextBox;
        }

        public void Log(string text, params object[] additionalData)
        {
            if (richTextBox != null)
            {
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
