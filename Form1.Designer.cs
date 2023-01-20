namespace Tinfoil_Resource_Downloader
{
    partial class Form1
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
            this.gameIcon = new System.Windows.Forms.PictureBox();
            this.gameIdInput = new System.Windows.Forms.TextBox();
            this.gameNameLabel = new System.Windows.Forms.Label();
            this.gameIdLabel = new System.Windows.Forms.Label();
            this.gameIdInputButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gameIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // gameIcon
            // 
            this.gameIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameIcon.Location = new System.Drawing.Point(594, 89);
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
            this.gameNameLabel.Location = new System.Drawing.Point(589, 24);
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
            this.gameIdLabel.Location = new System.Drawing.Point(591, 41);
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
            this.gameIdInputButton.Click += new System.EventHandler(this.gameIdInputButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 682);
            this.Controls.Add(this.gameIdInputButton);
            this.Controls.Add(this.gameIdLabel);
            this.Controls.Add(this.gameNameLabel);
            this.Controls.Add(this.gameIdInput);
            this.Controls.Add(this.gameIcon);
            this.Name = "Form1";
            this.Text = "Tinfoil Resource Downloader";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gameIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox gameIcon;
        private System.Windows.Forms.TextBox gameIdInput;
        private System.Windows.Forms.Label gameNameLabel;
        private System.Windows.Forms.Label gameIdLabel;
        private System.Windows.Forms.Button gameIdInputButton;
    }
}

