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
using System.Threading.Tasks;
using System.Windows.Forms;
using AudioSwitcher.AudioApi.CoreAudio;
using NAudio.CoreAudioApi;
using WinSoundMixer.App.SettingScreens;
using WinSoundMixer.App.Helpers;

namespace WinSoundMixer.App
{
    public partial class WinSoundMixer : Form
    {
        private Settings settings;
        private const string SettingsFileName = "Setting.json";

        private AudioWebSocketServer _server;
        private VolumeManager volumeManager;
        private AudioDeviceManager _deviceManager;
        private Timer updateTimer;
        private ContextMenuStrip dropdownMenu;
        private ContextMenuStrip devicesMenu;
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip notifyContextMenu;
        private LanguageManager languageManager;

        string show = "Göster";
        string close = "close";
        string txtSettings = "Settings";
        string txtHelp = "Help";
        string txtContactUs = "Contact Us";

        string lblClientCountTxt = "connected Devices";

        public WinSoundMixer()
        {
            InitializeComponent();
            AddControlsToFlowLayoutPanel();
            LoadSettings();
            ApplyCurrentTheme();
            _deviceManager = new AudioDeviceManager();
            volumeManager = new VolumeManager();
            _server  = new AudioWebSocketServer();

            LogHelper.Log("Application started", LogLevel.Info);

            _server.OnClientCountChanged += UpdateClientCount;

            updateTimer = new Timer();
            updateTimer.Interval = 500;
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();

            dropdownMenu = new ContextMenuStrip();
            dropdownMenu.ShowImageMargin = false;
            dropdownMenu.BackColor = Color.FromArgb(32, 32, 32);
            dropdownMenu.Renderer = new CustomRenderer();
            dropdownMenu.ForeColor = Color.White;
            var settingItem = dropdownMenu.Items.Add(txtSettings);
            var helpItem = dropdownMenu.Items.Add(txtHelp);
            var ContactUsItem = dropdownMenu.Items.Add(txtContactUs);

            settingItem.Click += SettingItem_Click;

            BtnMenu.MouseEnter += BtnMenu_MouseEnter;

            devicesMenu = new ContextMenuStrip();
            devicesMenu.ShowImageMargin = false;
            devicesMenu.BackColor = Color.FromArgb(32,32,32);
            devicesMenu.Renderer = new CustomRenderer();
            devicesMenu.ForeColor = Color.White;
            LoadDevices();
            BtnDevices.MouseEnter += BtnDevices_MouseEnter;

        }

        private void SettingItem_Click (object sender, EventArgs e)
        {
            FrmSetting setting = new FrmSetting();

            setting.Show();

        }
        private void UpdateClientCount(int clientCount)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateClientCount(clientCount)));
            }
            else
            {
                if (clientCount != 0)
                {
                    //lblClientCount.ForeColor = ColorTranslator.FromHtml("#ffffff");
                    lblClientCount.Text = clientCount + " " +lblClientCountTxt;
                }
                else { lblClientCount.Text = ""; }
            }
        }
        private async void UpdateTimer_Tick(object sender, EventArgs e)
        {
            await UpdateFlowLayoutPanelAsync();
        }

        private async Task UpdateFlowLayoutPanelAsync()
        {
            var volumeManager = new VolumeManager();
            var activeSessions = await volumeManager.GetActiveAudioSessionsAsync();

            int scrollPosition = flowLayoutPanel1.VerticalScroll.Value;

            foreach (var session in activeSessions)
            {
                var control = flowLayoutPanel1.Controls
                    .OfType<SoundControlItem>()
                    .FirstOrDefault(c => c.AppName == session.ApplicationName);

                if (control != null)
                {
                    // Var olan kontrolü güncelle
                    control.Volume = (int)session.Volume;
                }
                else
                {
                    SoundControlItem soundControlItem = new SoundControlItem
                    {
                        AppName = session.ApplicationName,
                        Volume = (int)session.Volume
                    };
                    flowLayoutPanel1.Controls.Add(soundControlItem);
                }
            }

            var controlsToRemove = flowLayoutPanel1.Controls
                .OfType<SoundControlItem>()
                .Where(c => !activeSessions.Any(s => s.ApplicationName == c.AppName))
                .ToList();

            foreach (var control in controlsToRemove)
            {
                flowLayoutPanel1.Controls.Remove(control);
            }

            flowLayoutPanel1.VerticalScroll.Value = Math.Min(scrollPosition, flowLayoutPanel1.VerticalScroll.Maximum);
        }




        public void LoadDevices()
        {
            var devices = _deviceManager.GetAudioDevices();
            devicesMenu.Items.Clear();

            foreach (var device in devices)
            {
                var item = new ToolStripMenuItem(device);
                item.Click += DeviceMenuItem_Click;
                devicesMenu.Items.Add(item);
            }
        }

        private void DeviceMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItem = sender as ToolStripMenuItem;
            if (selectedItem != null)
            {
                var deviceName = selectedItem.Text;
                _deviceManager.SetDefaultAudioDevice(deviceName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _server = new AudioWebSocketServer();
            notifyIcon = new NotifyIcon
            {
                Icon = this.Icon, // Simgeyi ayarla
                Visible = true,
                Text = this.Name // Sistem tepsisi simgesinin üzerine geldiğinde görünen metin
            };

            notifyContextMenu = new ContextMenuStrip();
            var showMenuItem = new ToolStripMenuItem(show);
            showMenuItem.Click += ShowNotifyMenuItem_Click;
            notifyContextMenu.ShowImageMargin = false;
            notifyContextMenu.BackColor = Color.FromArgb(32, 32, 32);
            notifyContextMenu.Renderer = new CustomRenderer();
            notifyContextMenu.ForeColor = Color.White;
            notifyContextMenu.Items.Add(showMenuItem);
            notifyContextMenu.Items.Add(new ToolStripMenuItem(close, null, ExitNotifyMenuItem_Click));

            notifyIcon.ContextMenuStrip = notifyContextMenu;

            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;

        }

        private void ShowNotifyMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void ExitNotifyMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            volumeManager.Dispose();
            _server?.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            BtnClose.BackColor = Color.Red;
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            BtnClose.BackColor = Color.Transparent;
        }

        private void BtnMinimize_MouseEnter(object sender, EventArgs e)
        {
            BtnMinimize.BackColor = Color.Transparent;
        }

        private void BtnMinimize_MouseLeave(object sender, EventArgs e)
        {
            BtnMinimize.BackColor= Color.Transparent;
        }

        private void BtnMenu_MouseEnter(object sender, EventArgs e)
        {
            dropdownMenu.Show(BtnMenu, new Point(0, BtnMenu.Height));
        }

        public class CustomRenderer : ToolStripProfessionalRenderer
        {
            // Kenarlık çizme işlemi devre dışı bırakıldı
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                // Kenarlığı çizme işleminden vazgeç
            }

            // Menü öğesi arka plan rengini değiştir
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (e.Item.Selected)
                {
                    // Seçili öğe arka planı (hover olduğunda)
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 0, 0, 0)), e.Item.ContentRectangle);  // Proje rengi #001645
                    e.Item.ForeColor = Color.White;  // Yazı rengini beyaz yap
                }
                else
                {
                    // Varsayılan arka plan rengi
                    e.Graphics.FillRectangle(new SolidBrush(e.ToolStrip.BackColor), e.Item.ContentRectangle);
                    e.Item.ForeColor = Color.LightGray;  // Varsayılan yazı rengi
                }
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

        private void flowLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                int rowHeight = flowLayoutPanel1.Controls[0].Height;

                // Kaydırma çubuğunu bir satır yüksekliği kadar ayarla
                flowLayoutPanel1.VerticalScroll.Value = Math.Min(flowLayoutPanel1.VerticalScroll.Maximum, (flowLayoutPanel1.VerticalScroll.Value / rowHeight) * rowHeight);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private async void AddControlsToFlowLayoutPanel()
        {

            VolumeManager volumeManager = new VolumeManager();
            var activeSessions = await volumeManager.GetActiveAudioSessionsAsync();

            flowLayoutPanel1.Controls.Clear(); // Önceki içerikleri temizle

            foreach (var session in activeSessions)
            {
                SoundControlItem soundControlItem = new SoundControlItem
                {
                    AppName = session.ApplicationName,
                    Volume = (int)session.Volume // TrackBar değeri genellikle 0-100 arası bir değerdir
                };

                flowLayoutPanel1.Controls.Add(soundControlItem);
            }
        }


        private void BtnDevices_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void BtnDevices_MouseClick(object sender, MouseEventArgs e)
        {
            devicesMenu.Show(BtnDevices, new Point(0, BtnDevices.Height));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            volumeManager.Dispose();
            _server?.Stop();
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.Hide();

            // Simgeyi göster
            notifyIcon.Visible = true;
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
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(settings.CurrentLanguage);
            // UI bileşenlerini güncelle
            BtnDevices.Text = languageManager.GetString("Devices");
            txtSettings = languageManager.GetString("Settings");
            txtHelp = languageManager.GetString("Help");
            txtContactUs = languageManager.GetString("ContactUs");
            lblClientCountTxt = languageManager.GetString("Connecteddevices");
            show = languageManager.GetString("Show");
            close = languageManager.GetString("Close");
        }

        private void ApplyCurrentTheme()
        {
            if (settings?.Themes != null)
            {
                var theme = settings.Themes[settings.CurrentTheme];

                // Formun arka plan rengini ve diğer kontrollerin renklerini güncelle
                this.BackColor = ColorTranslator.FromHtml(theme.Background);
                lblClientCount.ForeColor = ColorTranslator.FromHtml(theme.Color);
                LblAppName.ForeColor = ColorTranslator.FromHtml(theme.Color);
                BtnDevices.ForeColor = ColorTranslator.FromHtml(theme.Color);
                BtnDevices.BackColor = ColorTranslator.FromHtml(theme.Background) ;
                PnlTop.BackColor = ColorTranslator.FromHtml(theme.BackgroundTop);
                lblClientCount.ForeColor = ColorTranslator.FromHtml(theme.Color);
                // Butonların ikonlarını temaya göre güncelle
                UpdateButtonIcon(BtnClose, theme.Icons.Close);
                UpdateButtonIcon(BtnMinimize, theme.Icons.HorizontalLine);
                UpdateButtonIcon(BtnMenu, theme.Icons.Menu);
                // Diğer butonlar için ikon güncellemelerini buraya ekleyebilirsiniz
            }
        }
        private void OnThemeChanged(Theme newTheme)
        {
            ThemeHelper.ApplyTheme(this, newTheme, Path.Combine(Application.StartupPath, "Images"));
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
                MessageBox.Show($"Icon not found at: {iconPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogHelper.Log($"Icon not found at: {iconPath}", LogLevel.Error);
            }
        }

    }
}
