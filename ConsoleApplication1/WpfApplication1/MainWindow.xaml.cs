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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using ConsoleApplication1;
using System.Threading;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Interop;
using ConsoleApplication2;


namespace WpfApplication1
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Server server;
        private Boolean on;
        private Thread checker;
        private ObservableCollection<FileItemData> ListViewItemsCollections;
        private MailServer mailserver;
        public MainWindow()
        {
            
            InitializeComponent();
            mailserver = null;
            on = true;
            Console.SetOut(new ConsoleWriter(textBox1));
            server = new Server();
            textBox2.Text = "3555";
            checker = new Thread(() =>
            {
                DateTime DTdfp = Directory.GetLastWriteTime(Constants.defaultFilePath); while (on)
                {
                    DateTime cDTdfp = Directory.GetLastWriteTime(Constants.defaultFilePath);
                    if (!cDTdfp.Equals(DTdfp)) { Console.WriteLine(cDTdfp + "\n" + DTdfp); DTdfp = cDTdfp; } Thread.Sleep(1000);
                }
            });
            checker.Start();
            ListViewItemsCollections = new ObservableCollection<FileItemData>();
            foreach (FileInfo file in new DirectoryInfo(Constants.filePath).GetFiles())
            {
                String path = file.FullName;
                Icon icon = IconReader.GetFileIcon(path, IconReader.IconSize.Small, false);
                ImageSource img = Imaging.CreateBitmapSourceFromHIcon(
                            icon.Handle,
                            new Int32Rect(0, 0, icon.Width, icon.Height),
                            BitmapSizeOptions.FromEmptyOptions());
                ListViewItemsCollections.Add(new FileItemData()
                 {
                     icon = img,
                     location = path,
                     name = file.Name,
                     hidden = !((file.Attributes & FileAttributes.Hidden) == 0)
                 });
            }
            listView1.ItemsSource = ListViewItemsCollections;
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    String[] files = e.Data.GetData(DataFormats.FileDrop) as String[];
                    foreach (String path in files)
                    {
                        File.Copy(path, Util.getValidPath(new String[] { Constants.filePath, System.IO.Path.GetFileName(path) }));
                        if (File.Exists(path))
                            File.Delete(path);
                        Console.WriteLine(path);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //int port;
            //if (!int.TryParse(textBox2.Text, out port) || port < 1) MessageBox.Show("Invalid Number!");
            //else
            //{
            //    server.Start(port, false);
            //    swapBtnState(button1, button2);
            //}
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            server.Stop(false);
            if (Server.isStopped)
                swapBtnState(button1, button2);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MsgWin msgwin = new MsgWin("Exiting ...");
            msgwin.Show();
            on = false;
            if (server != null && !Server.isStopped)
                server.Stop(false);
            while (checker.IsAlive) Thread.Sleep(1);
            if (mailserver != null)
                mailserver.StopSmtp();
            msgwin.Close();
        }

        private void swapBtnState(Button b1, Button b2)
        {
            b1.IsEnabled = !b1.IsEnabled;
            b2.IsEnabled = !b2.IsEnabled;
        }

        private void GridView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            listView1.ContextMenu.IsEnabled = listView1.SelectedIndex > -1;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }


        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

            CheckBox source = e.Source as CheckBox;
            FileIO.setVisibility((source.DataContext as FileItemData).location, source.IsChecked.Value);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mailserver != null)
                {
                    button4.IsEnabled = false;
                    new Thread((w) => { mailserver.StopSmtp(); mailserver = null; button4.Dispatcher.Invoke((Action)(() => { button4.IsEnabled = true; button4.FontWeight = FontWeights.Normal; })); }).Start();
                }
                else
                {
                    mailserver = new MailServer("192.168.0.101", "moemoe.bear");
                    mailserver.smtpStart(3755);
                    button4.FontWeight = FontWeights.ExtraBold;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
            }
        }

        private class FileItemData
        {
            public ImageSource icon { get; set; }
            public String name { get; set; }
            public Boolean hidden { get; set; }
            public String location { get; set; }
        }
    }
}
