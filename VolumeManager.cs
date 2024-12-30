using CSCore.CoreAudioAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinSoundMixer.App.Helpers;

namespace WinSoundMixer.App
{
    internal class VolumeManager
    {
        private readonly SynchronizationContext _uiContext;

        public VolumeManager()
        {
            _uiContext = SynchronizationContext.Current;
        }

        public class AudioSessionInfo
        {
            public string ApplicationName { get; set; }
            public string SessionIdentifier { get; set; }
            public string SessionInstanceIdentifier { get; set; }
            public float Volume { get; set; }
            public uint ProcessId { get; set; }
        }

        public async Task<List<AudioSessionInfo>> GetActiveAudioSessionsAsync()
        {
            return await Task.Run(() =>
            {
                var sessions = new List<AudioSessionInfo>();
                using (var sessionManager = GetDefaultAudioSessionManager2(DataFlow.Render))
                using (var sessionEnumerator = sessionManager.GetSessionEnumerator())
                {
                    foreach (var session in sessionEnumerator)
                    {
                        using (var sessionControl = session.QueryInterface<AudioSessionControl2>())
                        using (var simpleAudioVolume = session.QueryInterface<SimpleAudioVolume>())
                        {
                            string appName = "Unknown";
                            int processId = sessionControl.ProcessID;

                            appName = GetApplicationName(processId);

                            string sessionId = sessionControl.SessionIdentifier;
                            string sessionInstanceId = sessionControl.SessionInstanceIdentifier;
                            float volume = simpleAudioVolume.MasterVolume;

                            sessions.Add(new AudioSessionInfo
                            {
                                ApplicationName = appName,
                                //SessionIdentifier = sessionId,
                                //SessionInstanceIdentifier = sessionInstanceId,
                                Volume = volume * 100,
                                ProcessId = (uint)processId
                            });
                        }
                    }
                }
                return sessions;
            });
        }
        public async Task<int> GetVolumeAsync(string appName)
    {
        return await Task.Run(() =>
        {
            using (var sessionManager = GetDefaultAudioSessionManager2(DataFlow.Render))
            using (var sessionEnumerator = sessionManager.GetSessionEnumerator())
            {
                foreach (var session in sessionEnumerator)
                {
                    using (var sessionControl = session.QueryInterface<AudioSessionControl2>())
                    using (var simpleAudioVolume = session.QueryInterface<SimpleAudioVolume>())
                    {
                        if (sessionControl.DisplayName.Equals(appName, StringComparison.OrdinalIgnoreCase))
                        {
                            return (int)(simpleAudioVolume.MasterVolume * 100);
                        }
                    }
                }
            }
            LogHelper.Log($"No audio session found for {appName}", LogLevel.Error);
            throw new Exception($"No audio session found for {appName}");
        });
    }

        public async Task SetVolumeAsync(string appName, int volume)
        {
            await Task.Run(() =>
            {
                using (var sessionManager = GetDefaultAudioSessionManager2(DataFlow.Render))
                using (var sessionEnumerator = sessionManager.GetSessionEnumerator())
                {
                    bool found = false;
                    foreach (var session in sessionEnumerator)
                    {
                        using (var sessionControl = session.QueryInterface<AudioSessionControl2>())
                        using (var simpleAudioVolume = session.QueryInterface<SimpleAudioVolume>())
                        {
                            string currentAppName = GetApplicationName(sessionControl.ProcessID);
                            if (currentAppName.Equals(appName, StringComparison.OrdinalIgnoreCase))
                            {
                                simpleAudioVolume.MasterVolume = volume / 100f;
                                found = true;
                                _uiContext.Post(_ => Console.WriteLine($"Volume set to {volume} for {appName}"), null);
                                break;
                            }
                        }
                    }
                    if (!found)
                    {
                        _uiContext.Post(_ => LogHelper.Log($"No matching session found for {appName}", LogLevel.Warn), null);
                    }
                }
            });
        }

        private string GetApplicationName(int processId)
        {
            if (processId == 0)
            {
                return "System Sounds";
            }

            try
            {
                using (var process = Process.GetProcessById(processId))
                {
                    // İşlemin hâlâ çalıştığını kontrol et
                    if (process != null && !process.HasExited)
                    {
                        // MainModule'ı güvenli şekilde eriş
                        if (process.MainModule != null)
                        {
                            return System.IO.Path.GetFileNameWithoutExtension(process.MainModule.FileName);
                        }
                        else
                        {
                            return $"Bilinmiyor (PID: {processId})";
                        }
                    }
                    else
                    {
                        return $"Bilinmiyor (PID: {processId})";
                    }
                }
            }
            catch (AccessViolationException)
            {
                // Erişim ihlali durumunu yönet
                return $"Erişim Reddedildi (PID: {processId})";
            }
            catch (Exception ex)
            {
                // Diğer hataları logla ve yönet
                LogHelper.Log($"Error while getting application name: {ex.Message}",LogLevel.Error);
                return $"Bilinmiyor (PID: {processId})";
            }
        }


        private static AudioSessionManager2 GetDefaultAudioSessionManager2(DataFlow dataFlow)
        {
            using (var enumerator = new MMDeviceEnumerator())
            using (var device = enumerator.GetDefaultAudioEndpoint(dataFlow, Role.Multimedia))
            {
                return AudioSessionManager2.FromMMDevice(device);
            }
        }

        public void Dispose()
        {
            // Dispose logic if needed
        }

       

    }
}
