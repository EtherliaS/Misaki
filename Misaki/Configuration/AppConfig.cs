﻿using Misaki.App;
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
    internal class AppConfig
    {
        public bool DebugInfo { get; set; }
        public bool SongDownloadInfo { get; set; }
        public AppVersion Appversion { get; }
        public AppConfig(bool debug, bool downloadinfo, AppVersion version)
        {
            DebugInfo = false;
            SongDownloadInfo = false;
            Appversion = version;
        }
        public AppConfig() { }
    }
}