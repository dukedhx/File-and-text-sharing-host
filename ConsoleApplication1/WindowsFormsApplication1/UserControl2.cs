using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class UserControl2 : UserControl4
    {
        public UserControl2()
        {
            InitializeComponent();
        }

        public override String addr { get { return textBox1.Text; }
            set { if (System.IO.Directory.Exists(value))textBox1.Text = value; } }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK) textBox1.Text = fbd.SelectedPath;
        }
    }
}
