using Misaki.Configuration;
using Misaki.Python;
using Misaki.Resources;
using Misaki.Utilities;
using Misaki.Utilities.JsonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misaki.App
{
    internal class ApplicationInitializer : IDisposable
    {

        public ApplicationInitializer() { }
        public async Task Init()
        {
            Console.Clear();
            await Logger.Log("Loading Configuration...", InfoSource.Misaki);
            await LoadConfiguration();
            await Logger.Log("Setting up Python...", InfoSource.Misaki);
            await InstallPython();
        }


        public AppConfig? _appconfig { get; private set; }
        public BotConfig? _botconfig { get; private set; }
        private async Task LoadConfiguration()
        {
            if (!Directory.Exists(LocalVars.ConfigDirectoryPath)) Directory.CreateDirectory(LocalVars.ConfigDirectoryPath);
            if (!File.Exists(LocalVars.AppConfigPath))
            {
                try
                {
                    _appconfig = new AppConfig(false, true, new AppVersion(1, 0, 0, 'a')); // get version from server?
                    await JsonConverter.WriteJsonToFileAsync(_appconfig, LocalVars.AppConfigPath);
                }
                catch (Exception ex) // replicate on other files
                {
                    await Logger.Log("Error creating appconfig", InfoSource.Misaki, InfoType.Error);
                    await Logger.Log(ex.Message, InfoSource.Error, InfoType.Error);
                }
            }
            else _appconfig = await JsonConverter.ReadJsonFileAsync<AppConfig>(LocalVars.AppConfigPath);

            if (!File.Exists(LocalVars.BotConfigPath))
            {
                _botconfig = new BotConfig(ReadTokenFromShell());
                await JsonConverter.WriteJsonToFileAsync(_botconfig, LocalVars.BotConfigPath);
            }
            else _botconfig = await JsonConverter.ReadJsonFileAsync<BotConfig>(LocalVars.BotConfigPath);

        }
        public void Dispose() { }
        private string ReadTokenFromShell()
        {
            string? _token;
            do
            {
                Console.Write("Discord bot token: ");
                _token = Console.ReadLine();
            }
            while (_token.Length != 70);
            return _token;
        }
        public async Task InstallPython()
        {
            await ScriptsManager.InstallVenv();
        }
        public async Task CheckUpdate()
        {
            await Logger.Log("Checking for updates...", InfoSource.Misaki);
            await Logger.Log("Latest version", InfoSource.Misaki);
            // placeholder
        }

    }
}
