using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Resources;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Windows.Forms;
using WinSoundMixer.App.Helpers;
using Newtonsoft.Json;

namespace WinSoundMixer.App.SettingScreens
{
    public partial class SetAdvenced : UserControl
    {
        private Settings settings;
        private const string SettingsFileName = "Setting.json";
        private LanguageManager languageManager;
        int port;
        string version = "1.0.0";
        public SetAdvenced()
        {
            InitializeComponent();
            LoadSettings();
            ApplyCurrentTheme();
            string ipAdress= GetLocalIPAddress();   

            TxtIpAdress.Text = ipAdress;
            port = settings.Port;
            TxtPort.Text = port.ToString();
            txtLogPath.ReadOnly = true;
            string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "WinSoundMixer");
            string logFilePath = Path.Combine(logDirectory, "WinSoundMixer.Log.txt");
            txtLogPath.Text = logFilePath;

        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            // Hata mesajını logla
            LogHelper.Log("Local IP address not found!", LogLevel.Error);

            // Ardından exception fırlat
            throw new Exception("Local IP address not found!");
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
                settings = System.Text.Json.JsonSerializer.Deserialize<Settings>(json, options);
            }
            else
            {
                LogHelper.Log($"Settings file not found at: {settingsPath}", LogLevel.Error);
                settings = new Settings(); // Varsayılan ayarları oluştur
            }

            // Eğer LanguageManager sınıfı Settings tipini alıyorsa, bunu kullan
            languageManager = new LanguageManager(GetSettingsFilePath()); // Dil yöneticisini oluştur
            ApplyCurrentLanguage(); // Mevcut dili uygula
        }

        private void ApplyCurrentLanguage()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(settings.CurrentLanguage);
            // UI bileşenlerini güncelle
            LblIpAdress.Text = languageManager.GetString("IpAdress");
            LblLog.Text = languageManager.GetString("Logging");
            BtnUpdate.Text = languageManager.GetString("UpdateCheck");
            BtnSave.Text = languageManager.GetString("Save");
        }


        private void ApplyCurrentTheme()
        {
            if (settings?.Themes != null)
            {
                var theme = settings.Themes[settings.CurrentTheme];

                // Formun arka plan rengini ve diğer kontrollerin renklerini güncelle
                this.BackColor = ColorTranslator.FromHtml(theme.Background);
                LblIpAdress.ForeColor = ColorTranslator.FromHtml(theme.Color);
                LblLog.ForeColor = ColorTranslator.FromHtml(theme.Color);
                LblPort.ForeColor = ColorTranslator.FromHtml(theme.Color);
                BtnUpdate.ForeColor = ColorTranslator.FromHtml(theme.Color);
                BtnUpdate.BackColor = ColorTranslator.FromHtml(theme.Background);
                BtnUpdate.FlatAppearance.BorderColor = ColorTranslator.FromHtml(theme.Color);
                BtnSave.FlatAppearance.BorderColor = ColorTranslator.FromHtml(theme.Color);
                BtnSave.ForeColor = ColorTranslator.FromHtml(theme.Color);

                if(BtnUpdate.Enabled == false)
                {
                    BtnUpdate.BackColor = ColorTranslator.FromHtml(theme.Background);
                    BtnUpdate.FlatAppearance.BorderColor = ColorTranslator.FromHtml(theme.Color);
                }


                // TextBox'ların arka plan ve metin renklerini güncelle
                txtLogPath.BackColor = ColorTranslator.FromHtml(theme.Background);
                TxtIpAdress.BackColor = ColorTranslator.FromHtml(theme.Background);
                TxtPort.BackColor = ColorTranslator.FromHtml(theme.Background);
                txtLogPath.ForeColor = ColorTranslator.FromHtml(theme.Color);
                TxtIpAdress.ForeColor = ColorTranslator.FromHtml(theme.Color);
                TxtPort.ForeColor = ColorTranslator.FromHtml(theme.Color);
                lblversion.ForeColor = ColorTranslator.FromHtml(theme.Color);

                // Butonların ikonlarını temaya göre güncelle
                UpdateButtonIcon(BtnOpenFolder, theme.Icons.Folder);
                // Diğer butonlar için ikon güncellemelerini buraya ekleyebilirsiniz
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Log dosyasının bulunduğu klasör
            string logDirectory = Path.GetDirectoryName(LogHelper.GetLogFilePath());

            // Klasörü aç
            if (Directory.Exists(logDirectory))
            {
                Process.Start("explorer.exe", logDirectory);
            }
            else
            {
                MessageBox.Show("Log dosyası klasörü bulunamadı.");
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

        private void SetAdvenced_Load(object sender, EventArgs e)
        {
            ApplyCurrentLanguage();
            lblversion.Text = version;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string currentVersion = version;
            string updateInfoUrl = "http://192.168.1.92:3000/version";

            using (WebClient client = new WebClient())
            {
                string json  = client.DownloadString(updateInfoUrl);
                var updateInfo = JsonConvert.DeserializeObject<UpdateInfo>(json);

                if (updateInfo.Version != currentVersion)
                {
                    DialogResult dialogResult = MessageBox.Show("Yeni bir güncelleme mevcut. Güncellemek ister misiniz?", "Güncelleme Mevcut", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        progressBar1.Visible = true;
                        DownloadUpdate(updateInfo.UpdateUrl);
                    }
                }
                else
                {
                    MessageBox.Show("Uygulamanız güncel.");
                }
            }
        }

        private void DownloadUpdate(string updateUrl)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += Client_DownloadProgressChanged;
                client.DownloadFileCompleted += Client_DownloadFileCompleted;

                // İndirme işlemi
                progressBar1.Value = 0; // İlerleme çubuğunu sıfırla
                client.DownloadFileAsync(new Uri(updateUrl), "update.zip"); // Dosyayı indir
            }
        }
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // İlerleme çubuğunu güncelle
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Güncelleme indirilirken hata oluştu: " + e.Error.Message);
                return;
            }

            // İndirme tamamlandığında yapılacak işlemler
            MessageBox.Show("Güncelleme başarıyla indirildi!");

            // Yardımcı uygulamayı başlat
            StartUpdater(); // İndirilen dosyanın tam yolunu gönder
        }

        private void StartUpdater()
        {
            string appDirectory = @"C:\Users\Administrator\Desktop\yusuf\WinSoundMixer\WinSoundMixerProject\WinSoundMixer.App\WinSoundMixer.Updater\bin\Debug\";
            string updaterPath = Path.Combine(appDirectory, "WinSoundMixer.Updater.exe");
            string updateZipPath = Path.Combine(@"C:\Users\Administrator\Desktop\yusuf\WinSoundMixer\WinSoundMixerProject\WinSoundMixer.App\WinSoundMixer.App\bin\x64\Debug\", "update.zip");

            if (!File.Exists(updaterPath))
            {
                MessageBox.Show("Yardımcı uygulama bulunamadı: " + updaterPath);
                return;
            }

            if (!File.Exists(updateZipPath))
            {
                MessageBox.Show("Güncelleme dosyası bulunamadı: " + updateZipPath);
                return;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo(updaterPath)
            {
                Arguments = $"\"{updateZipPath}\" \"{appDirectory}\"",
                WorkingDirectory = appDirectory,
                UseShellExecute = true,
                Verb = "runas" // Yönetici olarak çalıştır
            };

            try
            {
                Process.Start(startInfo);

                // Ana uygulamayı kapat
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Updater başlatılırken hata oluştu: " + ex.Message);
            }
        }

        private void TxtPort_TextChanged(object sender, EventArgs e)
        {

        }
        private void SaveSettings()
        {
            string settingsPath = GetSettingsFilePath();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string json = System.Text.Json.JsonSerializer.Serialize(settings, options);
            File.WriteAllText(settingsPath, json);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (int.TryParse(TxtPort.Text, out int newPort) && newPort > 0 && newPort < 65536)
            {
                DialogResult result = MessageBox.Show("Uygulamayı yeniden başlatmalısınız!", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    settings.Port = newPort; // Yeni port numarasını ayarla
                    SaveSettings(); // Settings.json'a kaydet
                    LogHelper.Log($"Port başarıyla ayarlandı: {settings.Port}", LogLevel.Info);
                    RestartApplication();
                }
                else
                {
                    TxtPort.Text = settings.Port.ToString();
                }
            }
            else
            {
                MessageBox.Show("Geçerli bir port numarası girin. (1-65535 arası)");
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


    }
    public class UpdateInfo
    {
        public string Version { get; set; }
        public string UpdateUrl { get; set; }
    }

}
