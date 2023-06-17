using System;

namespace Tinfoil_Resource_Downloader
{
    partial class Window2
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window2));
            this.gameIcon = new System.Windows.Forms.PictureBox();
            this.gameIdInput = new System.Windows.Forms.TextBox();
            this.gameNameLabel = new System.Windows.Forms.Label();
            this.gameIdLabel = new System.Windows.Forms.Label();
            this.gameIdInputButton = new System.Windows.Forms.Button();
            this.downloadType = new System.Windows.Forms.ListBox();
            this.gameBanner = new System.Windows.Forms.PictureBox();
            this.gameScreenshots = new System.Windows.Forms.PictureBox();
            this.downloadButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.screnshotsLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gameIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameBanner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameScreenshots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gameIcon
            // 
            this.gameIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameIcon.Location = new System.Drawing.Point(948, 59);
            this.gameIcon.Name = "gameIcon";
            this.gameIcon.Size = new System.Drawing.Size(200, 200);
            this.gameIcon.TabIndex = 0;
            this.gameIcon.TabStop = false;
            // 
            // gameIdInput
            // 
            this.gameIdInput.Location = new System.Drawing.Point(35, 99);
            this.gameIdInput.Name = "gameIdInput";
            this.gameIdInput.Size = new System.Drawing.Size(100, 20);
            this.gameIdInput.TabIndex = 1;
            this.gameIdInput.Text = "Input Game ID";
            // 
            // gameNameLabel
            // 
            this.gameNameLabel.AutoSize = true;
            this.gameNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameNameLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.gameNameLabel.Location = new System.Drawing.Point(945, 21);
            this.gameNameLabel.Name = "gameNameLabel";
            this.gameNameLabel.Size = new System.Drawing.Size(96, 17);
            this.gameNameLabel.TabIndex = 2;
            this.gameNameLabel.Text = "Game Name";
            this.gameNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gameIdLabel
            // 
            this.gameIdLabel.AutoSize = true;
            this.gameIdLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.gameIdLabel.Location = new System.Drawing.Point(947, 38);
            this.gameIdLabel.Name = "gameIdLabel";
            this.gameIdLabel.Size = new System.Drawing.Size(49, 13);
            this.gameIdLabel.TabIndex = 3;
            this.gameIdLabel.Text = "Game ID";
            this.gameIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gameIdInputButton
            // 
            this.gameIdInputButton.Location = new System.Drawing.Point(141, 99);
            this.gameIdInputButton.Name = "gameIdInputButton";
            this.gameIdInputButton.Size = new System.Drawing.Size(75, 23);
            this.gameIdInputButton.TabIndex = 4;
            this.gameIdInputButton.Text = "Search";
            this.gameIdInputButton.UseVisualStyleBackColor = true;
            this.gameIdInputButton.Click += new System.EventHandler(this.GameIdInputButton_Click);
            // 
            // downloadType
            // 
            this.downloadType.FormattingEnabled = true;
            this.downloadType.Items.AddRange(new object[] {
            "Download All",
            "Download Icon",
            "Download Banner",
            "Download Screenshots",
            "Download JSON File"});
            this.downloadType.Location = new System.Drawing.Point(35, 128);
            this.downloadType.Name = "downloadType";
            this.downloadType.Size = new System.Drawing.Size(181, 69);
            this.downloadType.TabIndex = 5;
            // 
            // gameBanner
            // 
            this.gameBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameBanner.Location = new System.Drawing.Point(522, 293);
            this.gameBanner.Name = "gameBanner";
            this.gameBanner.Size = new System.Drawing.Size(640, 360);
            this.gameBanner.TabIndex = 6;
            this.gameBanner.TabStop = false;
            // 
            // gameScreenshots
            // 
            this.gameScreenshots.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameScreenshots.Location = new System.Drawing.Point(35, 352);
            this.gameScreenshots.Name = "gameScreenshots";
            this.gameScreenshots.Size = new System.Drawing.Size(426, 240);
            this.gameScreenshots.TabIndex = 7;
            this.gameScreenshots.TabStop = false;
            this.gameScreenshots.Click += new System.EventHandler(this.GameScreenshots_Click);
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(35, 203);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(83, 24);
            this.downloadButton.TabIndex = 8;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(522, 274);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Banner";
            // 
            // screnshotsLabel
            // 
            this.screnshotsLabel.AutoSize = true;
            this.screnshotsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.screnshotsLabel.Location = new System.Drawing.Point(32, 336);
            this.screnshotsLabel.Name = "screnshotsLabel";
            this.screnshotsLabel.Size = new System.Drawing.Size(178, 13);
            this.screnshotsLabel.TabIndex = 9;
            this.screnshotsLabel.Text = "Screenshots (Click to change)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(85, 85);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(100, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(288, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Tinfoil Resource Downloader";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 660);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "An app by @AKASGaming";
            // 
            // Window2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 682);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.screnshotsLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.gameScreenshots);
            this.Controls.Add(this.gameBanner);
            this.Controls.Add(this.downloadType);
            this.Controls.Add(this.gameIdInputButton);
            this.Controls.Add(this.gameIdLabel);
            this.Controls.Add(this.gameNameLabel);
            this.Controls.Add(this.gameIdInput);
            this.Controls.Add(this.gameIcon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1190, 721);
            this.Name = "Window2";
            this.Text = "Tinfoil Resource Downloader";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gameIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameBanner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameScreenshots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox gameIcon;
        private System.Windows.Forms.TextBox gameIdInput;
        private System.Windows.Forms.Label gameNameLabel;
        private System.Windows.Forms.Label gameIdLabel;
        private System.Windows.Forms.Button gameIdInputButton;
        private System.Windows.Forms.ListBox downloadType;
        private System.Windows.Forms.PictureBox gameBanner;
        private System.Windows.Forms.PictureBox gameScreenshots;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label screnshotsLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

