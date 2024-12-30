using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using WinSoundMixer.App.Helpers;

namespace WinSoundMixer.App
{
    public class LanguageManager
    {
        public string CurrentLanguage { get; private set; }
        private ResourceManager resourceManager;

        public LanguageManager(string settingsFilePath)
        {
            LoadSettingsFromFile(settingsFilePath);
            resourceManager = new ResourceManager("WinSoundMixer.App.Localization.Strings", typeof(LanguageManager).Assembly);
        }

        public void LoadSettingsFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var settings = JsonSerializer.Deserialize<Settings>(json, options);
                CurrentLanguage = settings.CurrentLanguage; // Ayarları al
                ApplyCurrentLanguage(); // Dili uygula
            }
            else
            {
                LogHelper.Log($"Settings file not found at: {filePath}", LogLevel.Error);
            }
        }

        public void ApplyCurrentLanguage()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(CurrentLanguage);
        }

        public string GetString(string key)
        {
            var value = resourceManager.GetString(key);
            if (value == null)
            {
                // Log or handle the missing key situation
                LogHelper.Log($"Key '{key}' not found in language resources.", LogLevel.Error);
                return key; // Veya varsayılan bir değer dönebilir
            }
            return value;
        }
    }

}
