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
    public partial class UserControl4 : UserControl
    {
        protected String name; public String vName { get { return name; } set { name = value; } }
        public virtual String addr { get;  set; }
        public UserControl4()
        {
            InitializeComponent();
        }
    }
}
