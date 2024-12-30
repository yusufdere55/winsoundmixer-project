namespace WinSoundMixer.App
{
    partial class SoundControlItem
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
            this.TBSound = new System.Windows.Forms.TrackBar();
            this.BtnMute = new System.Windows.Forms.Button();
            this.LblAppName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TBSound)).BeginInit();
            this.SuspendLayout();
            // 
            // TBSound
            // 
            this.TBSound.Location = new System.Drawing.Point(80, 70);
            this.TBSound.Maximum = 100;
            this.TBSound.Name = "TBSound";
            this.TBSound.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TBSound.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TBSound.Size = new System.Drawing.Size(45, 259);
            this.TBSound.TabIndex = 0;
            this.TBSound.TickFrequency = 50;
            // 
            // BtnMute
            // 
            this.BtnMute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnMute.FlatAppearance.BorderSize = 0;
            this.BtnMute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMute.Location = new System.Drawing.Point(75, 335);
            this.BtnMute.Name = "BtnMute";
            this.BtnMute.Size = new System.Drawing.Size(50, 50);
            this.BtnMute.TabIndex = 1;
            this.BtnMute.UseVisualStyleBackColor = true;
            // 
            // LblAppName
            // 
            this.LblAppName.BackColor = System.Drawing.Color.Transparent;
            this.LblAppName.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblAppName.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAppName.ForeColor = System.Drawing.Color.White;
            this.LblAppName.Location = new System.Drawing.Point(0, 20);
            this.LblAppName.Name = "LblAppName";
            this.LblAppName.Size = new System.Drawing.Size(200, 30);
            this.LblAppName.TabIndex = 2;
            this.LblAppName.Text = "label1";
            this.LblAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SoundControlItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.Controls.Add(this.LblAppName);
            this.Controls.Add(this.BtnMute);
            this.Controls.Add(this.TBSound);
            this.Name = "SoundControlItem";
            this.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.Size = new System.Drawing.Size(200, 405);
            ((System.ComponentModel.ISupportInitialize)(this.TBSound)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar TBSound;
        private System.Windows.Forms.Button BtnMute;
        private System.Windows.Forms.Label LblAppName;
    }
}
