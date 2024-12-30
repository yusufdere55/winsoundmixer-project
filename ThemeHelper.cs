using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinSoundMixer.App
{
    public static class ThemeHelper
    {

        public static void ApplyTheme(Control parent, Theme theme, string iconsPath)
        {
            if (parent == null || theme == null) return;

            parent.BackColor = ColorTranslator.FromHtml(theme.Background);
            parent.ForeColor = ColorTranslator.FromHtml(theme.Color);

            UpdateIcons(parent, theme, iconsPath);

            foreach (Control control in parent.Controls)
            {
                if (control is Button button)
                {
                    string iconName = GetIconName(button.Name);
                    if (iconName != null)
                    {
                        string iconPath = Path.Combine(iconsPath, GetIconPath(iconName, theme));
                        if (File.Exists(iconPath))
                        {
                            button.BackgroundImage = Image.FromFile(iconPath);
                            button.BackgroundImageLayout = ImageLayout.Stretch; // veya uygun bir layout seçeneği
                        }
                    }
                }

                if (control.HasChildren)
                {
                    ApplyTheme(control, theme, iconsPath); // Recursive call for nested controls
                }
            }
        }

        private static void UpdateIcons(Control parent, Theme theme, string iconsPath)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button button)
                {
                    string iconName = GetIconName(button.Name);
                    if (iconName != null)
                    {
                        string iconPath = Path.Combine(iconsPath, GetIconPath(iconName, theme));
                        if (File.Exists(iconPath))
                        {
                            button.BackgroundImage = Image.FromFile(iconPath);
                            button.BackgroundImageLayout = ImageLayout.Stretch; // veya uygun bir layout seçeneği
                        }
                    }
                }

                if (control.HasChildren)
                {
                    UpdateIcons(control, theme, iconsPath);
                }
            }
        }

        private static string GetIconName(string controlName)
        {
            return controlName switch
            {
                "audioButton" => "Audio",
                "noAudioButton" => "NoAudio",
                "menuButton" => "Menu",
                "folderButton" => "Folder",
                "horizontalLineButton" => "HorizontalLine",
                "qrCodeButton" => "QRCode",
                _ => null
            };
        }

        private static string GetIconPath(string iconName, Theme theme)
        {
            return iconName switch
            {
                "Audio" => theme.Icons.Audio,
                "NoAudio" => theme.Icons.NoAudio,
                "Menu" => theme.Icons.Menu,
                "Folder" => theme.Icons.Folder,
                "HorizontalLine" => theme.Icons.HorizontalLine,
                "QRCode" => theme.Icons.QRCode,
                "CLose" => theme.Icons.Close,
                _ => throw new ArgumentException("Unknown icon name")
            };
        }
    }

}
