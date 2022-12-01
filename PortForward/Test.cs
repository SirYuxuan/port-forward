using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortForward
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            textBox1.Text = File.ReadAllText(@"C:\Users\Administrator\Desktop\New Text Document.txt");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllText(@"C:\Users\Administrator\Desktop\New Text Document.txt", textBox1.Text);
        }
    }
}
