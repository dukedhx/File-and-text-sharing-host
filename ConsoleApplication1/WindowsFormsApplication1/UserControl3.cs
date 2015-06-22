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
    public partial class UserControl3 : UserControl4
    {
        public UserControl3()
        {
            InitializeComponent();
        }
        public override String addr { get { int value; return int.TryParse(customControl11.Text, out value) && value > 0 ? customControl11.Text : null; } set { int tmp; if (int.TryParse(value, out tmp) && tmp > 0)customControl11.Text = value; } }
        public String label { get { return groupBox1.Text; } set { groupBox1.Text = value; } }
    }
}
