using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MsgWin.xaml
    /// </summary>
    public partial class MsgWin : Window
    {
        public MsgWin(String text)
        {
            InitializeComponent();
            label1.Content = text;
        }
    }
}
