using System;
using System.IO;
using System.Text.Json;
using WinSoundMixer.App.Helpers;

namespace WinSoundMixer.App
{
    public static class SettingsManager
    {
        private const string SettingsFile = "setting.json";

        public static Settings LoadSettings()
        {
            // Dosya yolunu direkt kullan
            if (File.Exists(SettingsFile))
            {
                string json = File.ReadAllText(SettingsFile);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // JSON'u deserialize et ve ayarları döndür
                return JsonSerializer.Deserialize<Settings>(json, options);
            }
            else
            {
                // Log ile hata mesajı göster ve varsayılan ayarları oluştur
                LogHelper.Log($"Settings file not found at: {SettingsFile}", LogLevel.Error);
                return new Settings(); // Varsayılan ayarları döndür
            }
        }

        public static void SaveSettings(Settings settings)
        {
            // JSON olarak serialize et ve dosyaya kaydet
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFile, json);
        }
    }
}
