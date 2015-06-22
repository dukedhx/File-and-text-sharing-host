using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Controls;
using System.Windows.Threading;


namespace WpfApplication1
{
    class ConsoleWriter : TextWriter
    {
        private TextBox textbox;
        public ConsoleWriter(TextBox tb)
        {
            textbox = tb;
        }

        public override void Write(char value)
        {
           textbox.Dispatcher.Invoke((Action)(() =>
    {
        textbox.Text += value;
    }));
           
        }

        public override void Write(string value)
        {
            textbox.Dispatcher.Invoke((Action)(() =>
    {
            textbox.Text += value;
    }));
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
