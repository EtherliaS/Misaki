using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Misaki.Utilities;

namespace Misaki.Python
{
    static class Shell
    {
        const string cmd = "cmd.exe";
        const string bash = "/bin/bash";
        const string args = "";
        const string activateVenvWin = "call \"./PythonResources/venv/Scripts/activate\"";
        const string activateVenvLnx = "source ./PythonResources/venv/Scripts/activate";
        public static string? Execute(string command, string args = "echo off", bool UseVenv = true, bool redirectoutput = true, bool redirectinput = true, bool redirecterror = true)
        {
            var startInfo = new ProcessStartInfo
            {
                RedirectStandardOutput = redirectoutput,
                RedirectStandardInput = redirectinput,
                RedirectStandardError = redirecterror,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = args,
                FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? cmd : bash
            };

            var process = Process.Start(startInfo);
            if (process == null)
            {
                Logger.Log("Could not start process: Shell process is null", InfoSource.Misaki, InfoType.Error);
                throw new Exception("Could not start process");
            }
            
            using var sw = process.StandardInput;
            if (sw.BaseStream.CanWrite)
            {
                if (UseVenv)
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) sw.WriteLine(activateVenvWin);
                    else sw.WriteLine(activateVenvLnx);
                }
                sw.WriteLine(cmd);
                sw.Flush();
                sw.Close();
            }
            if (redirectoutput)
            {
                using var reader = process.StandardOutput;
                
                var result = reader.ReadToEnd();
                //Console.Write(result);
                process.WaitForExit();
                using (StreamWriter ff = new("qwe.txt"))
                {
                    ff.Write(result);
                }
                process.StandardOutput.BaseStream.Flush();
                return result;
            }
            process.StandardOutput.BaseStream.Flush();
            process.WaitForExit();
            return null;

        }
        public static string? Execute(string[] command, string args = "", bool UseVenv = true, bool redirectoutput = true, bool redirectinput = true, bool redirecterror = true)
        {
            var startInfo = new ProcessStartInfo
            {
                RedirectStandardOutput = redirectoutput,
                RedirectStandardInput = redirectinput,
                RedirectStandardError = redirecterror,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = args,
                FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? cmd : bash
            };
            var process = Process.Start(startInfo);
            if (process == null)
            {
                Logger.Log("Could not start process: Shell process is null", InfoSource.Error);
                throw new Exception("Could not start process");
            }

            using var sw = process.StandardInput;
            if (sw.BaseStream.CanWrite)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    sw.WriteLine(activateVenvWin);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    sw.WriteLine(activateVenvLnx);
                }
                foreach (var cmd in command)
                {
                    sw.WriteLine(cmd);
                }
                sw.Flush();
                sw.Close();
            }
            if (redirectoutput)
            {
                using (var reader = process.StandardOutput)
                {
                    var result = reader.ReadToEnd();
                    Console.Write(result);
                    process.WaitForExit();
                    return result;
                }
            }
            process.WaitForExit();
            return null;

        }
    }
}
