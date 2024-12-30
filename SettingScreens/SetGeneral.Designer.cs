namespace WinSoundMixer.App.SettingScreens
{
    partial class SetGeneral
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
            this.LblRunonStrartup = new System.Windows.Forms.Label();
            this.CBLanguage = new System.Windows.Forms.ComboBox();
            this.CBRunOnStartup = new System.Windows.Forms.CheckBox();
            this.LblLanguage = new System.Windows.Forms.Label();
            this.LblTheme = new System.Windows.Forms.Label();
            this.CBTheme = new System.Windows.Forms.ComboBox();
            this.LblNotifications = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LblRunonStrartup
            // 
            this.LblRunonStrartup.AutoSize = true;
            this.LblRunonStrartup.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblRunonStrartup.ForeColor = System.Drawing.Color.White;
            this.LblRunonStrartup.Location = new System.Drawing.Point(51, 110);
            this.LblRunonStrartup.Name = "LblRunonStrartup";
            this.LblRunonStrartup.Size = new System.Drawing.Size(115, 19);
            this.LblRunonStrartup.TabIndex = 0;
            this.LblRunonStrartup.Tag = "RunOnStartup";
            this.LblRunonStrartup.Text = "Run on Startup";
            // 
            // CBLanguage
            // 
            this.CBLanguage.FormattingEnabled = true;
            this.CBLanguage.Location = new System.Drawing.Point(638, 31);
            this.CBLanguage.Name = "CBLanguage";
            this.CBLanguage.Size = new System.Drawing.Size(167, 21);
            this.CBLanguage.TabIndex = 1;
            this.CBLanguage.SelectedIndexChanged += new System.EventHandler(this.CBLanguage_SelectedIndexChanged_1);
            // 
            // CBRunOnStartup
            // 
            this.CBRunOnStartup.AutoSize = true;
            this.CBRunOnStartup.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CBRunOnStartup.Location = new System.Drawing.Point(209, 113);
            this.CBRunOnStartup.Name = "CBRunOnStartup";
            this.CBRunOnStartup.Size = new System.Drawing.Size(15, 14);
            this.CBRunOnStartup.TabIndex = 2;
            this.CBRunOnStartup.UseVisualStyleBackColor = true;
            this.CBRunOnStartup.CheckedChanged += new System.EventHandler(this.CBRunOnStartup_CheckedChanged);
            // 
            // LblLanguage
            // 
            this.LblLanguage.AutoSize = true;
            this.LblLanguage.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblLanguage.ForeColor = System.Drawing.Color.White;
            this.LblLanguage.Location = new System.Drawing.Point(456, 33);
            this.LblLanguage.Name = "LblLanguage";
            this.LblLanguage.Size = new System.Drawing.Size(144, 19);
            this.LblLanguage.TabIndex = 3;
            this.LblLanguage.Tag = "LanguageSelection";
            this.LblLanguage.Text = "Language Selection";
            // 
            // LblTheme
            // 
            this.LblTheme.AutoSize = true;
            this.LblTheme.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblTheme.ForeColor = System.Drawing.Color.White;
            this.LblTheme.Location = new System.Drawing.Point(51, 33);
            this.LblTheme.Name = "LblTheme";
            this.LblTheme.Size = new System.Drawing.Size(57, 19);
            this.LblTheme.TabIndex = 4;
            this.LblTheme.Tag = "Theme";
            this.LblTheme.Text = "Theme";
            // 
            // CBTheme
            // 
            this.CBTheme.FormattingEnabled = true;
            this.CBTheme.Location = new System.Drawing.Point(209, 33);
            this.CBTheme.Name = "CBTheme";
            this.CBTheme.Size = new System.Drawing.Size(167, 21);
            this.CBTheme.TabIndex = 5;
            this.CBTheme.SelectedIndexChanged += new System.EventHandler(this.CBTheme_SelectedIndexChanged);
            // 
            // LblNotifications
            // 
            this.LblNotifications.AutoSize = true;
            this.LblNotifications.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblNotifications.ForeColor = System.Drawing.Color.White;
            this.LblNotifications.Location = new System.Drawing.Point(51, 185);
            this.LblNotifications.Name = "LblNotifications";
            this.LblNotifications.Size = new System.Drawing.Size(100, 19);
            this.LblNotifications.TabIndex = 6;
            this.LblNotifications.Tag = "Notifications";
            this.LblNotifications.Text = "Notifications";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox2.Location = new System.Drawing.Point(209, 189);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // SetGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.LblNotifications);
            this.Controls.Add(this.CBTheme);
            this.Controls.Add(this.LblTheme);
            this.Controls.Add(this.LblLanguage);
            this.Controls.Add(this.CBRunOnStartup);
            this.Controls.Add(this.CBLanguage);
            this.Controls.Add(this.LblRunonStrartup);
            this.Name = "SetGeneral";
            this.Size = new System.Drawing.Size(850, 415);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblRunonStrartup;
        private System.Windows.Forms.ComboBox CBLanguage;
        private System.Windows.Forms.CheckBox CBRunOnStartup;
        private System.Windows.Forms.Label LblLanguage;
        private System.Windows.Forms.Label LblTheme;
        private System.Windows.Forms.ComboBox CBTheme;
        private System.Windows.Forms.Label LblNotifications;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}
