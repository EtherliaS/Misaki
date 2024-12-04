using Misaki.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misaki.MusicSources.Youtube
{
    public static class YoutubeDownloader
    {
        private static void EnsureDirectoryExists(string filepath)
        {
            string? directory = Path.GetDirectoryName(filepath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }


        public static void Download(string url, string savedir = "Videos/")
        {
            EnsureDirectoryExists(savedir);
            var youtube = new CustomYoutube();
            var videos = youtube.GetAllVideosAsync(url).GetAwaiter().GetResult();            
            var maxResolution = videos.First(i => i.Resolution <= 720);
            Console.ForegroundColor = ConsoleColor.Yellow;
            youtube.CreateDownloadAsync(
                new Uri(maxResolution.Uri),
                Path.Combine(savedir, maxResolution.FullName),
                new Progress<Tuple<long, long>>((v) =>
                {  
                    var percent = (int)(v.Item1 * 100 / v.Item2);
                    Console.Write(string.Format("Downloading... ( % {0} ) {1} / {2} MB\r", percent, (v.Item1 / (double)(1024 * 1024)).ToString("N"), (v.Item2 / (double)(1024 * 1024)).ToString("N")));
                }))
                .GetAwaiter().GetResult();
        }
    }
}
