using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConsoleApplication1;
using ConsoleApplication2;
using System.Diagnostics;
using System.Threading;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Server server; private MailServer mailserver;
        private Boolean on;
        public Form1()
        {
          
            InitializeComponent();           
            mailserver = null;
            on = true;           
                Console.SetOut(new ConsoleWriter(textBox1));
            server = null;    
            Dictionary<String, String> dict = SettingIO.load();
            if (Util.validateIE(dict)) foreach (UserControl4 uc in this.Controls.OfType<UserControl4>()) { 
                String value; if(dict.TryGetValue(uc.vName,out value))                    uc.addr=value; }           
            comboBox1.DataSource = IntlHelper.ShowNetworkInterfaceMessage();
            comboBox1.SelectedIndexChanged += (s, e) => { if(comboBox1.SelectedIndex>-1)userControl11.addr = comboBox1.Text; };
            if (!String.IsNullOrEmpty(comboBox1.Text)) userControl11.addr = comboBox1.Text;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (on)
                SettingIO.checkDB();
            String addr = userControl11.addr;
            try
            {
                if (server == null || server.isStopped)
                    if (String.IsNullOrEmpty(addr))
                    {
                        MessageBox.Show("Invalid IP"); return;
                    }
                    else
                    {
                        server = String.IsNullOrEmpty(userControl21.addr) ? new Server() : new Server(userControl21.addr);
                        server.Start(addr, false);
                    }
                else { Console.WriteLine("[Server]Server Terminating..."); server.Stop(checkBox2.Checked); server = null; }
                toggleBold(button1);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void toggleBold(Button button)
        {
            button.Font =new Font(button.Font, button.Font.Bold? FontStyle.Regular:FontStyle.Bold);           
        }
       
        private void button3_Click(object sender, EventArgs e)
        {           
            Process.Start(server==null?Constants.dot.ToString():server.filePath);
        }      

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                if (mailserver != null)
                {
                    mailserver.StopAll(); mailserver = null;
                }
                else

                    if (!(String.IsNullOrEmpty(userControl11.addr) || String.IsNullOrEmpty(userControl31.addr) || String.IsNullOrEmpty(userControl32.addr)))
                    {
                        mailserver = new MailServer(userControl11.addr, Constants.MailDomain); mailserver.setPath(userControl21.addr);
                        mailserver.smtpStart(Int32.Parse(userControl31.addr)); mailserver.pop3Start(Int32.Parse(userControl32.addr));
                        save();
                    }
                    else return;
                toggleBold(button4);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(userControl21.addr)) { if(server != null)server.setPath(userControl21.addr); save(userControl21.vName, userControl21.addr); }
        }

        private void save(String name,String value)
        {
            if (checkBox1.Checked) SettingIO.save(name,value);
        }

        private void save()
        {
            if (checkBox1.Checked)
            {
                Dictionary<String, String> dict = new Dictionary<String, String>();
                foreach (UserControl4 uc in this.Controls.OfType<UserControl4>())
                    dict.Add(uc.vName, uc.addr);
                SettingIO.save(dict);
            }
        }

        private void Window_Closing(object sender, FormClosingEventArgs e)
        {
            MsgWin msgwin = new MsgWin("Exiting ...");
            msgwin.Show();
            on = false;
            while(true)
            try
            {
                if (server != null && !server.isStopped)
                    server.Stop(true);
                //while (checker.IsAlive) Thread.Sleep(1);
                if (mailserver != null)
                    mailserver.StopAll();
                break;
            }
            catch (Exception ex) { if (MessageBox.Show(ex.Message, "Try exit again?", MessageBoxButtons.OKCancel) == DialogResult.Cancel) { e.Cancel = true; break; } }
            msgwin.Close();          
        }   

    }
}
