using System;
using System.Windows.Forms;

namespace Tinfoil_Resource_Downloader
{
    public partial class StartWindow : Form
    {
        public StartWindow()
        {
            InitializeComponent();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new Window2();
            form2.Closed += (s, args) => this.Close();
            form2.StartPosition = FormStartPosition.CenterScreen;
            form2.MaximizeBox = false;
            form2.Show();
        }
    }
}
