using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using WinSoundMixer.App.Helpers;

namespace WinSoundMixer.App
{
    internal class AudioDeviceManager
    {
        private readonly CoreAudioController _controller;

        public AudioDeviceManager()
        {
            _controller = new CoreAudioController();
        }

        public List<string> GetAudioDevices()
        {
            var devices = _controller.GetPlaybackDevices();
            return devices.Select(d => d.Name).ToList();
        }

        public void SetDefaultAudioDevice(string deviceName)
        {
            var devices = _controller.GetPlaybackDevices();
            var device = devices.FirstOrDefault(d => d.Name.Equals(deviceName, StringComparison.OrdinalIgnoreCase));
            if (device != null)
            {
                _controller.SetDefaultDeviceAsync(device);
                LogHelper.Log($"Device '{deviceName}' has been set as default.", LogLevel.Info);
            }
            else
            {
                LogHelper.Log($"Device '{deviceName}' not found.", LogLevel.Error);
            }
        }
    }
}
