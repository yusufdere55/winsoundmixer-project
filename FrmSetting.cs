using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WinSoundMixer.App.Helpers;
using WinSoundMixer.App.SettingScreens;

namespace WinSoundMixer.App
{
    public partial class FrmSetting : Form
    {
        private Settings settings;
        private const string SettingsFileName = "Setting.json";

        private NotifyIcon notifyIcon;

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        private LanguageManager languageManager;
        string AppName = "Settings";
        public FrmSetting()
        {
            InitializeComponent();
            LoadSettings();
            ApplyCurrentTheme();
            SetGeneral general = new SetGeneral();
            PnlScreen.Controls.Add(general);
            general.Show();
            LblAppName.Text= "WinSoundMixer " + AppName;
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
            BtnAdvenced.Text = languageManager.GetString("Advanced");
            BtnGeneral.Text = languageManager.GetString("General");
            BtnMobileApp.Text = languageManager.GetString("MobileApp");
            AppName = languageManager.GetString("Settings");
        }

        private void ApplyCurrentTheme()
        {
            if (settings?.Themes != null)
            {
                var theme = settings.Themes[settings.CurrentTheme];

                // Formun arka plan rengini ve diğer kontrollerin renklerini güncelle
                this.BackColor = ColorTranslator.FromHtml(theme.Background);
                LblAppName.ForeColor = ColorTranslator.FromHtml(theme.Color);
                PnlTop.BackColor = ColorTranslator.FromHtml(theme.BackgroundTop);
                BtnAdvenced.ForeColor = ColorTranslator.FromHtml(theme.Color);
                BtnGeneral.ForeColor = ColorTranslator.FromHtml(theme.Color);
                BtnMobileApp.ForeColor = ColorTranslator.FromHtml(theme.Color);

                // Butonların ikonlarını temaya göre güncelle
                UpdateButtonIcon(BtnClose, theme.Icons.Close);
                UpdateButtonIcon(BtnConnection, theme.Icons.QRCode);
                // Diğer butonlar için ikon güncellemelerini buraya ekleyebilirsiniz
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastCursor = Cursor.Position;
            lastForm = this.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point delta = Point.Subtract(Cursor.Position, new Size(lastCursor));
                this.Location = Point.Add(lastForm, new Size(delta));
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            BtnClose.BackColor = Color.Red;
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            BtnClose.BackColor = Color.Transparent;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnGeneral_Click(object sender, EventArgs e)
        {
            SetGeneral general = new SetGeneral();

            PnlScreen.Controls.Clear();
            PnlScreen.Controls.Add(general);
            general.Show();
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            SetGeneral general = new SetGeneral();
            PnlScreen.Controls.Add(general);
            general.Show();

            notifyIcon = new NotifyIcon
            {
                Icon = this.Icon, // Simgeyi ayarla
                Visible = true,
                Text = this.Name // Sistem tepsisi simgesinin üzerine geldiğinde görünen metin
            };
        }

        private void BtnAdvenced_Click(object sender, EventArgs e)
        {
            SetAdvenced advenced = new SetAdvenced();
            PnlScreen.Controls.Clear();
            PnlScreen.Controls.Add(advenced);
            advenced.Show();
        }

        private void BtnMobileApp_Click(object sender, EventArgs e)
        {
            SetMobileApp mobileApp = new SetMobileApp();
            PnlScreen.Controls.Clear();
            PnlScreen.Controls.Add(mobileApp);
            //mobileApp.Show();
        }

        private void ShowUserControl(UserControl control)
        {
            PnlScreen.Controls.Clear();
            PnlScreen.Controls.Add(control);
            control.Dock = DockStyle.Fill;
            control.Show();
        }

        private void PnlScreen_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnConnection_Click(object sender, EventArgs e)
        {
            SetConnection setConnection = new SetConnection(); // UserControl oluşturuluyor.
            PnlScreen.Controls.Clear(); // Panelin içi temizleniyor.
            PnlScreen.Controls.Add(setConnection); // UserControl panele ekleniyor.
        }
    }
}
