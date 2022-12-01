using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortForward.Util
{
    class LogUtil
    {
        private readonly TextBox logTextBox;
        private LogUtil(TextBox textBox)
        {
            this.logTextBox = textBox;
        }
        public static LogUtil GetLog(TextBox textBox)
        {
            return new LogUtil(textBox);

        }

        public void WriteLog(string text)
        {
            string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            logTextBox.Text += now + ": " + text + "\r\n";
            logTextBox.Select(this.logTextBox.TextLength, 0);
            logTextBox.ScrollToCaret();
        }
    }
}
