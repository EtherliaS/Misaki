using Misaki.Resources;
using Misaki.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misaki.Python
{
    public static class ScriptsManager
    {

        public static async Task InstallVenv(bool debug = false)
        {
            if (!Directory.Exists(LocalVars.PythonVenvPath))
            {
                await Logger.Log("Virtual environment not found, creating...", InfoSource.Python);
                Shell.Execute([$"cd {LocalVars.PythonResourcesPath}", "python -m venv venv"], UseVenv: false, redirectoutput: debug);
                await Logger.Log("Venv created!", InfoSource.Python);
            }
            await Logger.Log("Checking libraries...", InfoSource.Python);
            Shell.Execute([$"cd {LocalVars.PythonResourcesPath}", "pip install -r requirements.txt"], redirectoutput: debug);
            await Logger.Log("Python initialized successfully!", InfoSource.Python);
        }
        public static string? GetDownloadLink(string VideoID)
        {
            return Shell.Execute($"Python/Resources/GetDownloadLink.py {VideoID}");
        }
        public static string? DownloadVideo(string VideoID)
        {
            string? result = Shell.Execute($"Python/Resources/DownloadVideo.py {VideoID}");
            Shell.Execute($"ffmpeg.exe -i Music/{VideoID}.mp4 Music/{VideoID}.mp3");
            return result;
        }
        public static List<string> ParsePlaylist(string PlaylistURL)
        {
            List<String?> resulta = Shell.Execute("python Python/Resources/ParsePlaylist.py " + PlaylistURL).Split(PlaylistURL)[1].Split("(venv)")[0].Split("\n").ToList();
            List<string> links = new List<string>();
            for (int i = 0; i < resulta.Count; i++)
            {
                if (resulta[i].Contains(@"https://youtube.com/watch?")) links.Add(resulta[i]);
            }
            /*using (StreamWriter writer = new StreamWriter("text.txt", false))
            {
                foreach (var item in links)
                    writer.WriteLine(item);
            }*/
            return links;
        }
    }
}
