using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class CustomControl1 : TextBox
    {
        private Boolean initZero;
        public Boolean InitZero { get { return initZero; } set { initZero = value; } }
        public CustomControl1()
        {
            InitializeComponent();
            initZero = true;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            Char value = e.KeyChar;
            if ((Char.IsControl(value)||Char.IsDigit(value)) && ((String.IsNullOrEmpty(this.Text)&&initZero&&value.Equals('0'))||!"0".Equals(this.Text)))
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }
    }
}
