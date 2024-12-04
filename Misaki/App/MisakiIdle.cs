using Misaki.Configuration;
using Misaki.Discord;
using Misaki.Resources;
using Misaki.Utilities;
using Misaki.Python;
using Misaki.MusicSources.Youtube;

namespace Misaki.App
{
    class MisakiIdle
    {
        private static void StartMessage()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\r\n_________________________________________________\r\n    _   _      __     __     __     _    _     __\r\n    /  /|      /    /    )   / |    /  ,'      / \r\n   /| / |     /     \\       /__|   /_.'       /  \r\n  / |/  |    /       \\     /   |  /  \\       /   \r\n_/  /   |_ _/_   (____/  _/    |_/    \\_   _/_   \r\n______________________{_config.Appversion.ToString()}_____________________  \r\n                                                 \r");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static AppConfig _config;
        private static Bot MisakiBot;
        static async Task Main(string[] args)
        {

            using (var Initializer = new ApplicationInitializer())
            {
                await Initializer.Init();
                MisakiBot = new(Initializer._botconfig);
                _config = Initializer._appconfig;
            }
            StartMessage();
            await Logger.Log("Starting discord bot...", InfoSource.Misaki);
            //FIX CONNECTION
            //await MisakiBot.Start();
            //some test code
            YoutubeDownloader.Download("https://www.youtube.com/watch?v=ApdkzQOTKO8");
        }
    }
}