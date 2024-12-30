using Fleck;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinSoundMixer.App;
using WinSoundMixer.App.Helpers;

public class AudioWebSocketServer
{
    private readonly VolumeManager _volumeManager;
    private readonly AudioDeviceManager _deviceManager;
    private WebSocketServer _webSocketServer;
    private readonly List<IWebSocketConnection> _clients;
    private readonly Settings _settings;
    public event Action<int> OnClientCountChanged;

    public AudioWebSocketServer()
    {
        _settings = SettingsManager.LoadSettings();
        _volumeManager = new VolumeManager();
        _deviceManager = new AudioDeviceManager();
        _clients = new List<IWebSocketConnection>();

        _webSocketServer = new WebSocketServer($"ws://{GetLocalIPAddress()}:{_settings.Port}");
        _webSocketServer.Start(socket =>
        {
            socket.OnOpen = () =>
            {
                _clients.Add(socket);
                OnClientCountChanged?.Invoke(_clients.Count);
            };

            socket.OnClose = () =>
            {
                _clients.Remove(socket);
                OnClientCountChanged?.Invoke(_clients.Count);
            };
            socket.OnMessage = async message =>
            {
                LogHelper.Log($"New message received: {message}", WinSoundMixer.App.Helpers.LogLevel.Info); ;
                await HandleMessageAsync(socket, message);
            };
        });
        LogHelper.Log($"WebSocket server started at ws://{GetLocalIPAddress()}:{_settings.Port}", WinSoundMixer.App.Helpers.LogLevel.Info);
    }

    public void Stop()
    {
        _webSocketServer.Dispose();
        _clients.Clear();
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
        LogHelper.Log("Local IP address not found!", WinSoundMixer.App.Helpers.LogLevel.Error);
        throw new Exception("Local IP address not found!");
    }

    private async Task HandleMessageAsync(IWebSocketConnection socket, string message)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                LogHelper.Log("The incoming message is empty or null.", WinSoundMixer.App.Helpers.LogLevel.Debug);
                throw new ArgumentException("Gelen mesaj boş veya null.");
            }

            var trimmedMessage = message.TrimStart();
            if (!trimmedMessage.StartsWith("{"))
            {
                //MessageBox.Show($"Geçersiz JSON başlangıcı. İlk karakter: '{trimmedMessage[0]}'");
                LogHelper.Log("The incoming message is not in a valid JSON format.", WinSoundMixer.App.Helpers.LogLevel.Info);
                throw new JsonException("The incoming message is not in a valid JSON format.");
            }

            var msg = JsonSerializer.Deserialize<WebSocketMessage>(message);
            LogHelper.Log($"Successfully parsed message type: {msg.Type}", WinSoundMixer.App.Helpers.LogLevel.Info);
            switch (msg.Type)
            {
                case "getAudioSessions":
                    await SendAudioSessionsAsync(socket);
                    break;
                case "setVolume":
                    var volumeData = JsonSerializer.Deserialize<VolumeChangeRequest>(msg.Data.ToString());
                    await _volumeManager.SetVolumeAsync(volumeData.AppName, volumeData.Volume);
                    LogHelper.Log($"Volume adjusted: {volumeData.AppName} - {volumeData.Volume}", WinSoundMixer.App.Helpers.LogLevel.Info);
                    await SendAudioSessionsAsync(socket);
                    break;
                case "getAudioDevices":
                    await SendAudioDevicesAsync(socket);
                    break;
                case "setDefaultDevice":
                    var deviceData = JsonSerializer.Deserialize<DefaultDeviceRequest>(msg.Data.ToString());
                    _deviceManager.SetDefaultAudioDevice(deviceData.DeviceName);
                    LogHelper.Log($"Default device set: {deviceData.DeviceName}",WinSoundMixer.App.Helpers.LogLevel.Info);
                    await SendAudioDevicesAsync(socket);
                    break;
                default:
                    LogHelper.Log($"Unknown message type: {msg.Type}", WinSoundMixer.App.Helpers.LogLevel.Warn);
                    break;
            }
        }
        catch (JsonException ex)
        {
            LogHelper.Log($"JSON parsing error: {ex.Message}", WinSoundMixer.App.Helpers.LogLevel.Error);
            await socket.Send($"Hata: Geçersiz JSON formatı. Lütfen isteğinizi kontrol edin.");
        }
        catch (Exception ex)
        {
            LogHelper.Log($"An error occurred while processing the message: {ex.Message}", WinSoundMixer.App.Helpers.LogLevel.Error);
            await socket.Send($"Hata: {ex.Message}");
        }
    }

    private async Task SendMessageAsync(IWebSocketConnection socket, string type, object data)
    {
        try
        {
            var message = JsonSerializer.Serialize(new WebSocketMessage
            {
                Type = type,
                Data = data
            });
            LogHelper.Log($"Message sent: {message}", WinSoundMixer.App.Helpers.LogLevel.Info);
            await socket.Send(message);
        }
        catch (Exception ex)
        {
            LogHelper.Log($"An error occurred while sending the message: {ex.Message}",WinSoundMixer.App.Helpers.LogLevel.Error);
        }
    }

    private async Task SendAudioSessionsAsync(IWebSocketConnection socket)
    {
        var sessions = await _volumeManager.GetActiveAudioSessionsAsync();
        await SendMessageAsync(socket, "audioSessions", sessions);
    }

    private async Task SendAudioDevicesAsync(IWebSocketConnection socket)
    {
        var devices = _deviceManager.GetAudioDevices();
        await SendMessageAsync(socket, "audioDevices", devices);
    }
}

public class WebSocketMessage
{
    public string Type { get; set; }
    public object Data { get; set; }
}

public class VolumeChangeRequest
{
    public string AppName { get; set; }
    public int Volume { get; set; }
}

public class DefaultDeviceRequest
{
    public string DeviceName { get; set; }
}