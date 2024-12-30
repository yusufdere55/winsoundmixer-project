﻿using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinSoundMixer.App.Helpers;
using WinSoundMixer.App.Properties;

namespace WinSoundMixer.App.SettingScreens
{
    public partial class SetMobileApp : UserControl
    {
        private Settings settings;
        private const string SettingsFileName = "Setting.json";
        private LanguageManager languageManager;

        public SetMobileApp()
        {
            InitializeComponent();
            LoadSettings();
            ApplyCurrentTheme();
        }
        private string GetSettingsFilePath()
        {
            string exePath = Application.ExecutablePath;
            string exeDirectory = Path.GetDirectoryName(exePath);
            string projectDirectory = Directory.GetParent(exeDirectory).Parent.Parent.FullName;
            return Path.Combine(projectDirectory, SettingsFileName);
        }

        private void LoadSettings()
        {
            string settingsPath = GetSettingsFilePath();
            if (File.Exists(settingsPath))
            {
                string json = File.ReadAllText(settingsPath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                settings = JsonSerializer.Deserialize<Settings>(json, options);
            }
            else
            {
                LogHelper.Log($"Settings file not found at: {settingsPath}", LogLevel.Error);
                settings = new Settings(); // Create default settings
            }

            languageManager = new LanguageManager(GetSettingsFilePath()); // Dil yöneticisini oluştur
            ApplyCurrentLanguage(); // Mevcut dili uygula
        }
        private void ApplyCurrentLanguage()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(settings.CurrentLanguage);
            // UI bileşenlerini güncelle
            LblText.Text = languageManager.GetString("DownloadQRCode");
        }

        private void ApplyCurrentTheme()
        {
            if (settings?.Themes != null)
            {
                var theme = settings.Themes[settings.CurrentTheme];

                // Formun arka plan rengini ve diğer kontrollerin renklerini güncelle
                this.BackColor = ColorTranslator.FromHtml(theme.Background);
                LblText.ForeColor = ColorTranslator.FromHtml(theme.Color);

            }
        }

        private void UpdateButtonIcon(Button button, string iconPathRelativeToTheme)
        {
            // İkon dosyalarının bulunduğu dizin
            string exePath = Application.ExecutablePath;
            string exeDirectory = Path.GetDirectoryName(exePath);
            string projectDirectory = Directory.GetParent(exeDirectory).Parent.Parent.FullName;
            // İkon dosyasının tam yolu
            string iconPath = Path.Combine(projectDirectory, iconPathRelativeToTheme);

            if (File.Exists(iconPath))
            {
                button.BackgroundImage = Image.FromFile(iconPath);
                button.BackgroundImageLayout = ImageLayout.Center; // veya uygun bir layout seçeneği
            }
            else
            {
                LogHelper.Log($"Icon not found at: {iconPath}", LogLevel.Error);
            }
        }


        private void SetMobileApp_Load(object sender, EventArgs e)
        {
            string url = "https://www.youtube.com/watch?v=0XPkicLKwfI";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qRCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(9);

            PictureBox qrCodePictureBox = new PictureBox
            {
                Image = qrCodeImage,
                Location = new Point(450, 30), // Konumunu ayarlayın
                Size = new Size(qrCodeImage.Width, qrCodeImage.Height), // Boyutunu ayarlayın
                SizeMode = PictureBoxSizeMode.AutoSize // Boyutların otomatik ayarlanmasını sağlar
            };

            // PictureBox'ı kontrolün Controls koleksiyonuna ekleme
            this.Controls.Add(qrCodePictureBox);
        }

        

    }
}