namespace WinSoundMixer.App
{
    partial class FrmSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetting));
            this.PnlTop = new System.Windows.Forms.Panel();
            this.BtnClose = new System.Windows.Forms.Button();
            this.LblAppName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtnMobileApp = new System.Windows.Forms.Button();
            this.BtnAdvenced = new System.Windows.Forms.Button();
            this.BtnConnection = new System.Windows.Forms.Button();
            this.BtnGeneral = new System.Windows.Forms.Button();
            this.lblClientCount = new System.Windows.Forms.Label();
            this.PnlScreen = new System.Windows.Forms.Panel();
            this.PnlTop.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlTop
            // 
            this.PnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.PnlTop.Controls.Add(this.BtnClose);
            this.PnlTop.Controls.Add(this.LblAppName);
            this.PnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlTop.Location = new System.Drawing.Point(0, 0);
            this.PnlTop.Name = "PnlTop";
            this.PnlTop.Size = new System.Drawing.Size(850, 35);
            this.PnlTop.TabIndex = 1;
            this.PnlTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.PnlTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.PnlTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // BtnClose
            // 
            this.BtnClose.BackColor = System.Drawing.Color.Transparent;
            this.BtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnClose.FlatAppearance.BorderSize = 0;
            this.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClose.Location = new System.Drawing.Point(815, 0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(35, 35);
            this.BtnClose.TabIndex = 1;
            this.BtnClose.UseVisualStyleBackColor = false;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            this.BtnClose.MouseEnter += new System.EventHandler(this.BtnClose_MouseEnter);
            this.BtnClose.MouseLeave += new System.EventHandler(this.BtnClose_MouseLeave);
            // 
            // LblAppName
            // 
            this.LblAppName.AutoSize = true;
            this.LblAppName.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAppName.ForeColor = System.Drawing.Color.White;
            this.LblAppName.Location = new System.Drawing.Point(12, 9);
            this.LblAppName.Name = "LblAppName";
            this.LblAppName.Size = new System.Drawing.Size(150, 17);
            this.LblAppName.TabIndex = 1;
            this.LblAppName.Text = "WinSoundMixer Settings";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BtnMobileApp);
            this.panel2.Controls.Add(this.BtnAdvenced);
            this.panel2.Controls.Add(this.BtnConnection);
            this.panel2.Controls.Add(this.BtnGeneral);
            this.panel2.Controls.Add(this.lblClientCount);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 25);
            this.panel2.TabIndex = 3;
            // 
            // BtnMobileApp
            // 
            this.BtnMobileApp.AutoSize = true;
            this.BtnMobileApp.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnMobileApp.FlatAppearance.BorderSize = 0;
            this.BtnMobileApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMobileApp.ForeColor = System.Drawing.Color.White;
            this.BtnMobileApp.Location = new System.Drawing.Point(150, 0);
            this.BtnMobileApp.Name = "BtnMobileApp";
            this.BtnMobileApp.Size = new System.Drawing.Size(75, 25);
            this.BtnMobileApp.TabIndex = 7;
            this.BtnMobileApp.Tag = "MobileApp";
            this.BtnMobileApp.Text = "Mobile App";
            this.BtnMobileApp.UseVisualStyleBackColor = true;
            this.BtnMobileApp.Click += new System.EventHandler(this.BtnMobileApp_Click);
            // 
            // BtnAdvenced
            // 
            this.BtnAdvenced.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnAdvenced.FlatAppearance.BorderSize = 0;
            this.BtnAdvenced.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAdvenced.ForeColor = System.Drawing.Color.White;
            this.BtnAdvenced.Location = new System.Drawing.Point(75, 0);
            this.BtnAdvenced.Name = "BtnAdvenced";
            this.BtnAdvenced.Size = new System.Drawing.Size(75, 25);
            this.BtnAdvenced.TabIndex = 6;
            this.BtnAdvenced.Tag = "Advanced ";
            this.BtnAdvenced.Text = "Advanced ";
            this.BtnAdvenced.UseVisualStyleBackColor = true;
            this.BtnAdvenced.Click += new System.EventHandler(this.BtnAdvenced_Click);
            // 
            // BtnConnection
            // 
            this.BtnConnection.BackgroundImage = global::WinSoundMixer.App.Properties.Resources.QrCode;
            this.BtnConnection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnConnection.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnConnection.FlatAppearance.BorderSize = 0;
            this.BtnConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnConnection.ForeColor = System.Drawing.Color.White;
            this.BtnConnection.Location = new System.Drawing.Point(827, 0);
            this.BtnConnection.Name = "BtnConnection";
            this.BtnConnection.Size = new System.Drawing.Size(23, 25);
            this.BtnConnection.TabIndex = 5;
            this.BtnConnection.UseVisualStyleBackColor = true;
            this.BtnConnection.Click += new System.EventHandler(this.BtnConnection_Click);
            // 
            // BtnGeneral
            // 
            this.BtnGeneral.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnGeneral.FlatAppearance.BorderSize = 0;
            this.BtnGeneral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGeneral.ForeColor = System.Drawing.Color.White;
            this.BtnGeneral.Location = new System.Drawing.Point(0, 0);
            this.BtnGeneral.Name = "BtnGeneral";
            this.BtnGeneral.Size = new System.Drawing.Size(75, 25);
            this.BtnGeneral.TabIndex = 3;
            this.BtnGeneral.Tag = "General";
            this.BtnGeneral.Text = "General";
            this.BtnGeneral.UseVisualStyleBackColor = true;
            this.BtnGeneral.Click += new System.EventHandler(this.BtnGeneral_Click);
            // 
            // lblClientCount
            // 
            this.lblClientCount.AutoSize = true;
            this.lblClientCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblClientCount.ForeColor = System.Drawing.Color.White;
            this.lblClientCount.Location = new System.Drawing.Point(700, 6);
            this.lblClientCount.Name = "lblClientCount";
            this.lblClientCount.Size = new System.Drawing.Size(0, 16);
            this.lblClientCount.TabIndex = 2;
            this.lblClientCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PnlScreen
            // 
            this.PnlScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlScreen.Location = new System.Drawing.Point(0, 60);
            this.PnlScreen.Name = "PnlScreen";
            this.PnlScreen.Size = new System.Drawing.Size(850, 415);
            this.PnlScreen.TabIndex = 4;
            this.PnlScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlScreen_Paint);
            // 
            // FrmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.ClientSize = new System.Drawing.Size(850, 475);
            this.Controls.Add(this.PnlScreen);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSetting";
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.FrmSetting_Load);
            this.PnlTop.ResumeLayout(false);
            this.PnlTop.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlTop;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Label LblAppName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblClientCount;
        private System.Windows.Forms.Button BtnGeneral;
        private System.Windows.Forms.Panel PnlScreen;
        private System.Windows.Forms.Button BtnConnection;
        private System.Windows.Forms.Button BtnMobileApp;
        private System.Windows.Forms.Button BtnAdvenced;
    }
}