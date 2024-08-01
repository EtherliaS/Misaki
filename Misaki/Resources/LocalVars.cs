using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misaki.Resources
{
    public static class LocalVars
    {
        public const string ConfigDirectoryPath = "Configuration";
        public const string AppConfigPath = $@"{ConfigDirectoryPath}\AppConfig.json";
        public const string BotConfigPath = $@"{ConfigDirectoryPath}\BotConfig.json";
        public const string MusicPath = "Music";
        public const string PythonPath = "Python";
        public const string PythonResourcesPath = $@"{PythonPath}\Resources";
        public const string PythonVenvPath = $@"{PythonResourcesPath}\venv";
        public const string LogsPath = "Logs";
        public const string LatestLogPath = $@"{LogsPath}\latest.log";
        public const string LocalIP = "127.0.0.1";
    }
}
