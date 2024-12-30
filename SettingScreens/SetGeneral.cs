using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using WinSoundMixer.App.Helpers;

namespace WinSoundMixer.App.SettingScreens
{
    public partial class SetGeneral : UserControl
    {
        private Settings settings;
        private const string SettingsFileName = "Setting.json";
        private const string IconsPath = "Resources";
        private ResourceManager resourceManager;

        string ThemeInfo = "ThemeInfo";
        string LanguageInfo = "LanguageInfo";

        private string AppName = "WinSoundMixer";
        private string StartupRegisteryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        public SetGeneral()
        {
            InitializeComponent();
            LoadSettings();
            InitializeThemeComboBox();
            InitializeLanguageComboBox();
            ApplyCurrentTheme();
            ApplyCurrentLanguage();
            UpdateUI();

            LblLanguage.Tag = "LanguageSelection";
            LblTheme.Tag = "Theme";
            LblNotifications.Tag = "Notifications";
            LblRunonStrartup.Tag = "RunonStartup";


            CheckRunOnStartup();
        }

        private void EnebleStartup()
        {
            try
            {
                string exePath = Application.ExecutablePath;

                RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupRegisteryKey, true);
                key.SetValue(AppName, "\"" + exePath + "\"");
            }
            catch (Exception ex)
            {
                LogHelper.Log("Başlangıçta çalıştırılamadı: " + ex.Message, LogLevel.Error);
            }
        }

        private void CheckRunOnStartup()
        {
            try
            {
                if (string.IsNullOrEmpty(StartupRegisteryKey) || string.IsNullOrEmpty(AppName))
                {
                    LogHelper.Log("StartupRegisteryKey veya AppName boş.", LogLevel.Error);
                    SetCheckboxSafely(false);
                    return;
                }

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupRegisteryKey, false))
                {
                    if (key == null)
                    {
                        LogHelper.Log("Başlangıç anahtarı bulunamadı.", LogLevel.Error);
                        SetCheckboxSafely(false);
                        return;
                    }

                    object value = key.GetValue(AppName);
                    string exePath = Application.ExecutablePath;

                    if (exePath == null)
                    {
                        LogHelper.Log("Application.ExecutablePath null döndürdü.", LogLevel.Error);
                        SetCheckboxSafely(false);
                        return;
                    }

                    bool isRegistered = false;
                    if (value != null)
                    {
                        string registryValue = value.ToString().Trim('"');
                        isRegistered = string.Equals(registryValue, exePath, StringComparison.OrdinalIgnoreCase);
                    }

                    SetCheckboxSafely(isRegistered);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log("Başlangıçta çalıştırma kontrolü sırasında hata oluştu: " + ex.Message, LogLevel.Error);
                SetCheckboxSafely(false);
            }
        }

        private void SetCheckboxSafely(bool isChecked)
        {
            if (CBRunOnStartup.InvokeRequired)
            {
                CBRunOnStartup.Invoke(new Action(() => CBRunOnStartup.Checked = isChecked));
            }
            else
            {
                CBRunOnStartup.Checked = isChecked;
            }
        }

        private void DisableStartup()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupRegisteryKey, true);
                key.DeleteValue(AppName, false);
            }
            catch (Exception ex)
            {
                LogHelper.Log("Başlangıçtan Kaldırılamadı: " + ex.Message, LogLevel.Error);
            }
        }

        private string GetSettingsFilePath()
        {
            string exePath = Application.ExecutablePath;
            string exeDirectory = Path.GetDirectoryName(exePath);
            string projectDirectory = Directory.GetParent(exeDirectory).Parent.Parent.FullName;
            return Path.Combine(projectDirectory, SettingsFileName);
        }

        public void LoadSettings()
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
                MessageBox.Show($"Settings file not found at: {settingsPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                settings = new Settings(); // Create default settings if file not found
            }
            ApplyCurrentLanguage();
        }

        private void SaveSettings()
        {
            string settingsPath = GetSettingsFilePath();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string json = JsonSerializer.Serialize(settings, options);
            File.WriteAllText(settingsPath, json);
        }

        private void InitializeThemeComboBox()
        {
            if (settings?.Themes != null)
            {
                foreach (var theme in settings.Themes.Keys)
                {
                    CBTheme.Items.Add(theme);
                }
                CBTheme.SelectedItem = settings.CurrentTheme;
            }
        }

        private void CBTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedTheme = CBTheme.SelectedItem.ToString();
            if (settings.CurrentTheme != selectedTheme)
            {
                settings.CurrentTheme = selectedTheme;
                MessageBox.Show(ThemeInfo, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RestartApplication();
                SaveSettings();
                ApplyCurrentTheme();
            }
        }

        private void RestartApplication()
        {
            // Yeni bir process başlat
            Process.Start(new ProcessStartInfo()
            {
                FileName = Application.ExecutablePath,
                UseShellExecute = true
            });

            // Mevcut uygulamayı kapat
            Application.Exit();
        }

        private void ApplyCurrentTheme()
        {
            if (settings?.Themes != null)
            {
                var theme = settings.Themes[settings.CurrentTheme];
                this.BackColor = ColorTranslator.FromHtml(theme.Background);
                LblLanguage.ForeColor = ColorTranslator.FromHtml(theme.Color);
                LblNotifications.ForeColor = ColorTranslator.FromHtml(theme.Color);
                LblRunonStrartup.ForeColor = ColorTranslator.FromHtml(theme.Color);
                LblTheme.ForeColor = ColorTranslator.FromHtml(theme.Color);
            }
        }

        private void UpdateButtonBackgroundImage(Button button, string iconFileName)
        {
            string iconPath = Path.Combine(GetIconsFullPath(), iconFileName);
            if (File.Exists(iconPath))
            {
                button.BackgroundImage = Image.FromFile(iconPath);
                button.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private string GetIconsFullPath()
        {
            string exePath = Application.ExecutablePath;
            string exeDirectory = Path.GetDirectoryName(exePath);
            string projectDirectory = Directory.GetParent(exeDirectory).Parent.Parent.FullName;
            return Path.Combine(projectDirectory, IconsPath);
        }

        private void InitializeLanguageComboBox()
        {
            CBLanguage.Items.Add("English");
            CBLanguage.Items.Add("Türkçe");
            CBLanguage.Items.Add("Deutsch");
            //MessageBox.Show($"Mevcut Dil: {settings.CurrentLanguage}");
            CBLanguage.SelectedIndex = settings.CurrentLanguage == "tr" ? 1 :
                               settings.CurrentLanguage == "de" ? 2 : 0;
        }

        private void ApplyCurrentLanguage()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(settings.CurrentLanguage);

            // Dil ayarını kontrol et
            //MessageBox.Show($"UICulture: {Thread.CurrentThread.CurrentUICulture}");

            resourceManager = new ResourceManager("WinSoundMixer.App.Localization.Strings", typeof(SetGeneral).Assembly);
            LanguageInfo = resourceManager.GetString("LanguageInfo");
            ThemeInfo = resourceManager.GetString("ThemeInfo");
            // UI'yi güncelle
            UpdateUI();
        }


        private void UpdateUI()
        {
            UpdateControlTexts(this);
        }

        private void UpdateControlTexts(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Tag != null && !string.IsNullOrEmpty(c.Tag.ToString()))
                {
                    string resourceKey = c.Tag.ToString();
                    string translatedText = resourceManager.GetString(resourceKey);

                    // Hata ayıklama için
                    //MessageBox.Show($"Anahtar: {resourceKey}, Çekilen Metin: {translatedText}");

                    if (!string.IsNullOrEmpty(translatedText))
                    {
                        c.Text = translatedText;
                    }
                }

                if (c.HasChildren)
                {
                    UpdateControlTexts(c);
                }
            }
        }


        private void SetGeneral_Load(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void CBLanguage_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedLanguage = CBLanguage.SelectedIndex == 1 ? "tr" :
                                          CBLanguage.SelectedIndex == 2 ? "de" : "en";
            if (settings.CurrentLanguage != selectedLanguage)
            {
                settings.CurrentLanguage = selectedLanguage;
                MessageBox.Show(LanguageInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RestartApplication();
                SaveSettings();
                ApplyCurrentLanguage();
                UpdateUI();
            }
        }

        private void CBRunOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            if(CBRunOnStartup.Checked)
            {
                EnebleStartup();
            }
            else
            {
                DisableStartup();
            }
        }
    }

    public class Settings
    {
        public string CurrentTheme { get; set; }
        public string CurrentLanguage { get; set; }
        public int Port { get; set; }
        public Dictionary<string, Theme> Themes { get; set; }
    }

    public class Theme
    {
        public string Background { get; set; }
        public string BackgroundTop { get; set; }
        public string Color { get; set; }
        public string sounditem { get; set; }
        public string menu { get; set; }
        public Icons Icons { get; set; }
    }

    public class Icons
    {
        public string Audio { get; set; }
        public string NoAudio { get; set; }
        public string Menu { get; set; }
        public string Folder { get; set; }
        public string HorizontalLine { get; set; }
        public string QRCode { get; set; }
        public string Close { get; set; }
    }
}