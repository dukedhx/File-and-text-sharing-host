using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace WindowsFormsApplication1
{
    public partial class UserControl1 : UserControl4
    {
     
        private IEnumerable<CustomControl1> partlist;
        public UserControl1()
        {
            InitializeComponent();          
            partlist = this.Controls[0].Controls.Cast<CustomControl1>().Reverse();            
        }

        public override String addr
        {
            get {
                IEnumerator<CustomControl1> ie = partlist.GetEnumerator();
                String value = null;
                Boolean on = true;
                if(ie.MoveNext())
                while (on)
                {
                    String cv=null; Char prefix;
                    cv = ie.Current.Text;
                    if (String.IsNullOrEmpty(cv)) return null;
                    if (ie.MoveNext())
                        prefix = Constants.dot;
                    else
                    { on = false; prefix = Constants.colon; }  
                    value +=  String.IsNullOrEmpty(value)?cv:prefix+cv;                    
                }
               
                return validateIp(value)?value:null;
               // return replaceLast(".",":" ,String.Join(".", (from part in partlist select part.Text).Reverse().ToArray())); 
            }

            set { if (validateIp(value)) { String[] parts = value.Split(Constants.colon); IEnumerator<CustomControl1> ie = partlist.GetEnumerator(); foreach (String cv in parts[0].Split(Constants.dot))if (ie.MoveNext())ie.Current.Text = cv; if (ie.MoveNext() && parts.Length > 1) ie.Current.Text = parts[1]; } }
        }

        private Boolean validateIp(String ip)
        {
            IPAddress sb;
            return String.IsNullOrEmpty(ip) ? false : IPAddress.TryParse(ip.Split(Constants.colon)[0], out sb);
        }

        //private String replaceLast(String target, String replace, String source)
        //{
        //    int id = source.LastIndexOf(target);
        //    return id > -1 ? source.Remove(id, target.Length).Insert(id, replace) : source;
        //}
    }
}
