using Misaki.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misaki.Configuration
{
    enum LogLevel
    {
        Info,
        Warn,
        Error
    } // means nothing now, a placeholder for later expansion
    public class AppConfig
    {
        public bool DebugInfo { get; set; }
        public bool ClearLogs { get; set; }
        public int MaxLogFileCount { get; set; }
        public bool SongDownloadInfo { get; set; }
        public AppVersion Appversion = AppVersion.Parse("0.0.2d");
        public AppConfig(bool debug, bool downloadinfo, AppVersion version, bool clearlogs, int maxfcount)
        {
            DebugInfo = false;
            SongDownloadInfo = false;
            Appversion = version;
        }
        public AppConfig() { }
    }
}
