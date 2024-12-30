namespace WinSoundMixer.App.SettingScreens
{
    partial class SetMobileApp
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
            this.LblText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblText
            // 
            this.LblText.AutoSize = true;
            this.LblText.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblText.ForeColor = System.Drawing.Color.White;
            this.LblText.Location = new System.Drawing.Point(46, 154);
            this.LblText.Name = "LblText";
            this.LblText.Size = new System.Drawing.Size(385, 22);
            this.LblText.TabIndex = 0;
            this.LblText.Tag = "DownloadQRCode";
            this.LblText.Text = "You can download it by scanning the QR Code";
            // 
            // SetMobileApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.Controls.Add(this.LblText);
            this.Name = "SetMobileApp";
            this.Size = new System.Drawing.Size(850, 415);
            this.Load += new System.EventHandler(this.SetMobileApp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblText;
    }
}
