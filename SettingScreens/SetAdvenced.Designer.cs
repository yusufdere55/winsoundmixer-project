namespace WinSoundMixer.App.SettingScreens
{
    partial class SetAdvenced
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
            this.LblIpAdress = new System.Windows.Forms.Label();
            this.LblPort = new System.Windows.Forms.Label();
            this.TxtIpAdress = new System.Windows.Forms.TextBox();
            this.TxtPort = new System.Windows.Forms.TextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.LblLog = new System.Windows.Forms.Label();
            this.txtLogPath = new System.Windows.Forms.TextBox();
            this.BtnOpenFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblversion = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // LblIpAdress
            // 
            this.LblIpAdress.AutoSize = true;
            this.LblIpAdress.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblIpAdress.ForeColor = System.Drawing.Color.White;
            this.LblIpAdress.Location = new System.Drawing.Point(44, 39);
            this.LblIpAdress.Name = "LblIpAdress";
            this.LblIpAdress.Size = new System.Drawing.Size(75, 19);
            this.LblIpAdress.TabIndex = 5;
            this.LblIpAdress.Tag = "IpAdress";
            this.LblIpAdress.Text = "Ip Adress";
            // 
            // LblPort
            // 
            this.LblPort.AutoSize = true;
            this.LblPort.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblPort.ForeColor = System.Drawing.Color.White;
            this.LblPort.Location = new System.Drawing.Point(401, 39);
            this.LblPort.Name = "LblPort";
            this.LblPort.Size = new System.Drawing.Size(39, 19);
            this.LblPort.TabIndex = 6;
            this.LblPort.Text = "Port";
            // 
            // TxtIpAdress
            // 
            this.TxtIpAdress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.TxtIpAdress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtIpAdress.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.TxtIpAdress.ForeColor = System.Drawing.Color.White;
            this.TxtIpAdress.Location = new System.Drawing.Point(167, 40);
            this.TxtIpAdress.Name = "TxtIpAdress";
            this.TxtIpAdress.ReadOnly = true;
            this.TxtIpAdress.Size = new System.Drawing.Size(190, 23);
            this.TxtIpAdress.TabIndex = 7;
            // 
            // TxtPort
            // 
            this.TxtPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.TxtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPort.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.TxtPort.ForeColor = System.Drawing.Color.White;
            this.TxtPort.Location = new System.Drawing.Point(459, 40);
            this.TxtPort.Name = "TxtPort";
            this.TxtPort.Size = new System.Drawing.Size(190, 23);
            this.TxtPort.TabIndex = 8;
            this.TxtPort.TextChanged += new System.EventHandler(this.TxtPort_TextChanged);
            // 
            // BtnSave
            // 
            this.BtnSave.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnSave.ForeColor = System.Drawing.Color.White;
            this.BtnSave.Location = new System.Drawing.Point(690, 32);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(136, 37);
            this.BtnSave.TabIndex = 9;
            this.BtnSave.Tag = "Save";
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.BtnUpdate.Enabled = false;
            this.BtnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnUpdate.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnUpdate.ForeColor = System.Drawing.Color.White;
            this.BtnUpdate.Location = new System.Drawing.Point(302, 320);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(230, 50);
            this.BtnUpdate.TabIndex = 10;
            this.BtnUpdate.Tag = "UpdateCheck";
            this.BtnUpdate.Text = "Update Check";
            this.BtnUpdate.UseCompatibleTextRendering = true;
            this.BtnUpdate.UseVisualStyleBackColor = false;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // LblLog
            // 
            this.LblLog.AutoSize = true;
            this.LblLog.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblLog.ForeColor = System.Drawing.Color.White;
            this.LblLog.Location = new System.Drawing.Point(44, 141);
            this.LblLog.Name = "LblLog";
            this.LblLog.Size = new System.Drawing.Size(66, 19);
            this.LblLog.TabIndex = 11;
            this.LblLog.Tag = "Logging";
            this.LblLog.Text = "Logging";
            // 
            // txtLogPath
            // 
            this.txtLogPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.txtLogPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogPath.ForeColor = System.Drawing.Color.White;
            this.txtLogPath.Location = new System.Drawing.Point(167, 143);
            this.txtLogPath.Name = "txtLogPath";
            this.txtLogPath.Size = new System.Drawing.Size(482, 20);
            this.txtLogPath.TabIndex = 12;
            // 
            // BtnOpenFolder
            // 
            this.BtnOpenFolder.BackColor = System.Drawing.Color.Transparent;
            this.BtnOpenFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnOpenFolder.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BtnOpenFolder.FlatAppearance.BorderSize = 0;
            this.BtnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOpenFolder.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnOpenFolder.Location = new System.Drawing.Point(655, 136);
            this.BtnOpenFolder.Name = "BtnOpenFolder";
            this.BtnOpenFolder.Size = new System.Drawing.Size(30, 30);
            this.BtnOpenFolder.TabIndex = 13;
            this.BtnOpenFolder.UseVisualStyleBackColor = false;
            this.BtnOpenFolder.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 389);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Version";
            // 
            // lblversion
            // 
            this.lblversion.AutoSize = true;
            this.lblversion.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblversion.ForeColor = System.Drawing.Color.White;
            this.lblversion.Location = new System.Drawing.Point(66, 389);
            this.lblversion.Name = "lblversion";
            this.lblversion.Size = new System.Drawing.Size(34, 16);
            this.lblversion.TabIndex = 15;
            this.lblversion.Text = "1.0.0";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(199, 264);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(450, 50);
            this.progressBar1.TabIndex = 16;
            this.progressBar1.Visible = false;
            // 
            // SetAdvenced
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblversion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnOpenFolder);
            this.Controls.Add(this.txtLogPath);
            this.Controls.Add(this.LblLog);
            this.Controls.Add(this.BtnUpdate);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.TxtPort);
            this.Controls.Add(this.TxtIpAdress);
            this.Controls.Add(this.LblPort);
            this.Controls.Add(this.LblIpAdress);
            this.Name = "SetAdvenced";
            this.Size = new System.Drawing.Size(850, 415);
            this.Load += new System.EventHandler(this.SetAdvenced_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblIpAdress;
        private System.Windows.Forms.Label LblPort;
        private System.Windows.Forms.TextBox TxtIpAdress;
        private System.Windows.Forms.TextBox TxtPort;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnUpdate;
        private System.Windows.Forms.Label LblLog;
        private System.Windows.Forms.TextBox txtLogPath;
        private System.Windows.Forms.Button BtnOpenFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblversion;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}
