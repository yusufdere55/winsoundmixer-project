using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinSoundMixer.App.SettingScreens;

namespace WinSoundMixer.App
{
    public partial class SoundControlItem : UserControl
    {
        private Settings settings;
        private const string SettingsFileName = "Setting.json";

        private float lastVolume;
        private bool isMuted;
        private Image muteImage;
        private Image unmuteImage;

        public SoundControlItem()
        {
            InitializeComponent();
            LoadSettings();
            ApplyCurrentTheme();

            TBSound.ValueChanged += TBSound_ValueChanged;
            BtnMute.Click += MuteButton_Click;
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
                MessageBox.Show($"Settings file not found at: {settingsPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                settings = new Settings(); // Create default settings
            }
        }

        private void ApplyCurrentTheme()
        {
            if (settings?.Themes != null)
            {
                var theme = settings.Themes[settings.CurrentTheme];

                // Formun arka plan rengini ve diğer kontrollerin renklerini güncelle
                this.BackColor = ColorTranslator.FromHtml(theme.sounditem);
                LblAppName.ForeColor = ColorTranslator.FromHtml(theme.Color);
                string exePath = Application.ExecutablePath;
                string exeDirectory = Path.GetDirectoryName(exePath);
                string projectDirectory = Directory.GetParent(exeDirectory).Parent.Parent.FullName;
                muteImage = LoadImageFromPath(Path.Combine(projectDirectory, theme.Icons.NoAudio));
                unmuteImage = LoadImageFromPath(Path.Combine(projectDirectory, theme.Icons.Audio));
                string noAudioPath = Path.Combine(projectDirectory, theme.Icons.NoAudio);

                if (isMuted)
                {
                    BtnMute.BackgroundImage = muteImage;
                }
                else
                {
                    BtnMute.BackgroundImage = unmuteImage;
                }
            }
        }
        private Image LoadImageFromPath(string path)
        {
            if (File.Exists(path))
            {
                return Image.FromFile(path);
            }
            return null;
        }

        public string AppName
        {
            get => LblAppName.Text;
            set => LblAppName.Text = value;
        }

        public int Volume
        {
            get => TBSound.Value;
            set => TBSound.Value = value;
        }

        private async Task UpdateVolumeAsync(int volume)
        {
            VolumeManager volumeManager = new VolumeManager();
            await volumeManager.SetVolumeAsync(AppName, volume);
        }

        private async void MuteButton_Click(object sender, EventArgs e)
        {
            if (isMuted)
            {
                // Mute'dan çıkma işlemleri
                TBSound.Enabled = true;
                TBSound.Value = (int)lastVolume;
                BtnMute.BackgroundImage = unmuteImage;
                isMuted = false;
            }
            else
            {
                // Mute olma işlemleri
                lastVolume = TBSound.Value; // Mevcut sesi kaydet
                TBSound.Value = 0;
                TBSound.Enabled = false;
                BtnMute.BackgroundImage = muteImage;
                isMuted = true;
            }

            // Ses seviyesini güncelle
            await UpdateVolumeAsync(TBSound.Value);
        }

        private async void TBSound_ValueChanged(object sender, EventArgs e)
        {
            // TrackBar'ın değerini al
            int volume = TBSound.Value;

            // VolumeManager'ı kullanarak ses seviyesini ayarla
            await UpdateVolumeAsync(volume);
        }

        private string GetIconsFullPath()
        {
            string exePath = Application.ExecutablePath;
            string exeDirectory = Path.GetDirectoryName(exePath);
            string projectDirectory = Directory.GetParent(exeDirectory).Parent.Parent.FullName;
            return Path.Combine(projectDirectory, "Resources");
        }
    }
}
