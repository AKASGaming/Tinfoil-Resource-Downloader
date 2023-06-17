using AngleSharp.Io;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Windows.UI.Xaml.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Tinfoil_Resource_Downloader
{
    public partial class DownloadStatus : UserControl
    {


        public List<string> Log = new List<string>();

        public DownloadStatus()
        {
            InitializeComponent();
            button1.Enabled = false;
            button1.Text = "Please Wait...";
            
        }

        public void UpdateProgressBar(int progress)
        {
            this.progressBar1.BeginInvoke(
                (MethodInvoker)delegate ()
                {
                    progressBar1.Value = progress;
                    progressBar1.Refresh();
                }
            );
        }

        public void UpdateButton(bool value)
        {
            button1.Enabled = value;
            button1.Text = "Close";
        }

        public void UpdateStatus(string message)
        {
            this.Log.Add(string.Format("{0} - {1}", DateTime.Now, message));
            Output.WriteLine(message);
            StringBuilder result = new StringBuilder();
            foreach (string s in this.Log)
            {
                result.Append(string.Format("{0} \r\n", s));
            }
            this.richTextBox1.Text = result.ToString();
        }

        public void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
