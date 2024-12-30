using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSoundMixer.App
{
    public  class Settings
    {
        public string CurrentTheme { get; set; }
        public string CurrentLanguage { get; set; }
        public int Port { get; set; }
        public Dictionary<string, Theme> Themes { get; set; }
    }
}
