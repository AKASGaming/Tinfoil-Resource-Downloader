namespace Tinfoil_Resource_Downloader
{
    partial class DownloadStatus
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadStatus));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.IconStatus = new System.Windows.Forms.PictureBox();
            this.BannerStatus = new System.Windows.Forms.PictureBox();
            this.JSONStatus = new System.Windows.Forms.PictureBox();
            this.ScreenshotsStatus = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.IconStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BannerStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JSONStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScreenshotsStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(25, 454);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(433, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(165, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Downloading...";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(25, 298);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBox1.Size = new System.Drawing.Size(433, 150);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.RichTextBox1_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(337, 490);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // IconStatus
            // 
            this.IconStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.IconStatus.Image = ((System.Drawing.Image)(resources.GetObject("IconStatus.Image")));
            this.IconStatus.InitialImage = ((System.Drawing.Image)(resources.GetObject("IconStatus.InitialImage")));
            this.IconStatus.Location = new System.Drawing.Point(55, 75);
            this.IconStatus.Name = "IconStatus";
            this.IconStatus.Size = new System.Drawing.Size(50, 50);
            this.IconStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IconStatus.TabIndex = 4;
            this.IconStatus.TabStop = false;
            // 
            // BannerStatus
            // 
            this.BannerStatus.Image = ((System.Drawing.Image)(resources.GetObject("BannerStatus.Image")));
            this.BannerStatus.Location = new System.Drawing.Point(55, 193);
            this.BannerStatus.Name = "BannerStatus";
            this.BannerStatus.Size = new System.Drawing.Size(50, 50);
            this.BannerStatus.TabIndex = 5;
            this.BannerStatus.TabStop = false;
            // 
            // JSONStatus
            // 
            this.JSONStatus.Image = ((System.Drawing.Image)(resources.GetObject("JSONStatus.Image")));
            this.JSONStatus.Location = new System.Drawing.Point(283, 193);
            this.JSONStatus.Name = "JSONStatus";
            this.JSONStatus.Size = new System.Drawing.Size(50, 50);
            this.JSONStatus.TabIndex = 7;
            this.JSONStatus.TabStop = false;
            // 
            // ScreenshotsStatus
            // 
            this.ScreenshotsStatus.Image = ((System.Drawing.Image)(resources.GetObject("ScreenshotsStatus.Image")));
            this.ScreenshotsStatus.Location = new System.Drawing.Point(283, 75);
            this.ScreenshotsStatus.Name = "ScreenshotsStatus";
            this.ScreenshotsStatus.Size = new System.Drawing.Size(50, 50);
            this.ScreenshotsStatus.TabIndex = 6;
            this.ScreenshotsStatus.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(111, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Icon";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(111, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Banner";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(339, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Screenshots";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(339, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "JSON";
            // 
            // DownloadStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.JSONStatus);
            this.Controls.Add(this.ScreenshotsStatus);
            this.Controls.Add(this.BannerStatus);
            this.Controls.Add(this.IconStatus);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Name = "DownloadStatus";
            this.Size = new System.Drawing.Size(482, 527);
            ((System.ComponentModel.ISupportInitialize)(this.IconStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BannerStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JSONStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScreenshotsStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox IconStatus;
        private System.Windows.Forms.PictureBox BannerStatus;
        private System.Windows.Forms.PictureBox JSONStatus;
        private System.Windows.Forms.PictureBox ScreenshotsStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
