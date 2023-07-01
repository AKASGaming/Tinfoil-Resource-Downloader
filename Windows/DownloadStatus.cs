using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using Tinfoil_Resource_Downloader.Properties;

namespace Tinfoil_Resource_Downloader
{
    public partial class DownloadStatus : UserControl
    {


        public List<string> Log = new List<string>();

        public Form Window2 { get; set; }

        private Window2 parent;

        public Window2 GetParent()
        {
            return parent;
        }

        public void SetParent(Window2 value)
        {
            parent = value;
        }

        public bool Clicked {  get; set; }

        public DownloadStatus()
        {
            InitializeComponent();
            button1.Enabled = false;
            button1.Text = "Please Wait...";
        }

        public void SetWindow(Form window)
        {
            Window2 = window;
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

        public void UpdateIcon(string Icon, string Status)
        {
            Bitmap thisImage = null;
            switch (Status)
            {
                case "Wait":
                    thisImage = new Bitmap(Resources.not_downloaded);
                    thisImage = ChangeToColor(thisImage, Color.Gray);
                    break;
                case "Downloading":
                    thisImage = new Bitmap(Resources.downloading);
                    thisImage = ChangeToColor(thisImage, Color.DarkGoldenrod);
                    break;
                case "Finished":
                    thisImage = new Bitmap(Resources.finished_downloading);
                    thisImage = ChangeToColor(thisImage, Color.Green);
                    break;
            }

            switch (Icon)
            {
                case "Icon":
                    this.IconStatus.Image = thisImage;
                    break;
                case "Screenshots":
                    this.ScreenshotsStatus.Image = thisImage;
                    break;
                case "Banner":
                    this.BannerStatus.Image = thisImage;
                    break;
                case "JSON":
                    this.JSONStatus.Image = thisImage;
                    break;
            }
        }

        public Bitmap ChangeToColor(Bitmap bmp, Color c)
        {
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics g = Graphics.FromImage(bmp2))
            {
                float tr = c.R / 255f;
                float tg = c.G / 255f;
                float tb = c.B / 255f;

                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                  {
                 new float[] {0, 0, 0, 0, 0},
                 new float[] {0, 0, 0, 0, 0},
                 new float[] {0, 0, 0, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {tr, tg, tb, 0, 1}  // kudos to OP!
                  });

                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);

                g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height),
                    0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attributes);
            }
            return bmp2;
        }

        public void UpdateButton(bool value)
        {
            button1.Enabled = value;
            button1.Text = "Close";
        }

        public void UpdateStatus(string message)
        {
            this.Log.Add(string.Format("[{0}] - {1}", DateTime.Now, message));
            Output.WriteLine(message);
            StringBuilder result = new StringBuilder();
            foreach (string s in this.Log)
            {
                result.Append(string.Format("{0} \r\n", s));
            }
            this.richTextBox1.Text = result.ToString();
        }

        public void SetRunningStatus()
        {
            GetParent().running = false;
        }

        public void GetParentWindow(Window2 window)
        {
            SetParent(window);
        }

        public void Button1_Click(object sender, EventArgs e)
        {
            SetRunningStatus();
            Window2.Close();
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Visible)
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();
            }
        }
    }
}
