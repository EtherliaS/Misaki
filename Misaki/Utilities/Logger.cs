using Misaki.Resources;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misaki.Utilities
{
    enum InfoSource
    {
        Discord,
        Misaki,
        Error,
        Python
    }
    enum InfoType
    {
        Info,
        Warn,
        Debug,
        Error
    }
    static class Logger
    {
        private static bool NewLog = true;
        private static void CreateLogFile()
        {
            if (!Directory.Exists(LocalVars.LogsPath)) Directory.CreateDirectory(LocalVars.LogsPath);
            if (File.Exists(LocalVars.LatestLogPath))
            {
                File.Move(LocalVars.LatestLogPath, @$"{LocalVars.LogsPath}\{File.GetCreationTime(LocalVars.LatestLogPath).ToString().Replace(':', '-')}.log");
                File.Delete(LocalVars.LatestLogPath);
            }
            using (File.Create(LocalVars.LatestLogPath)) { }
        }
        private static async Task LogToFile(string message)
        {
            using (StreamWriter writer = new StreamWriter(LocalVars.LatestLogPath, true))
            {
                await writer.WriteLineAsync(message);
            }
        }
        public static async Task Log(string message, InfoSource source, InfoType infoType = InfoType.Info)
        {
            if (NewLog)
            {
                CreateLogFile();
                NewLog = false;
            }
            StringBuilder sb = new StringBuilder();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            var Date = DateTime.Now;
            Console.Write($"{Date.ToShortTimeString()}");
            sb.Append(Date);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" (");
            sb.Append(" (");
            switch (source)
            {
                case InfoSource.Discord:
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("Discord");
                        sb.Append("Discord");
                        break;
                    }
                case InfoSource.Misaki:
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("Misaki");
                        sb.Append("Misaki");
                        break;
                    }
                case InfoSource.Error:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Error");
                        sb.Append("Error");
                        break;
                    }
                case InfoSource.Python:
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Python");
                        sb.Append("Python");
                        break;
                    }
                default: break;
            }
            Console.ForegroundColor= ConsoleColor.DarkGray;
            Console.Write('|');
            switch (infoType)
            {
                case InfoType.Info:
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("Info");
                        sb.Append("Info");
                        break;
                    }
                case InfoType.Warn:
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("Warn");
                        sb.Append("Warn");
                        break;
                    }
                case InfoType.Debug:
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Debug");
                        sb.Append("Debug");
                        break;
                    }
                case InfoType.Error:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Error");
                        sb.Append("Error");
                        break;
                    }
                default: break;
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(") ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            sb.Append(message);
            await LogToFile(sb.ToString());
        }
    }
}
