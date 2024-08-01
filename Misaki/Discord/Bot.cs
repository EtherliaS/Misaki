using Discord;
using Discord.WebSocket;
using Misaki.Configuration;
using Misaki.Discord.Resources.Guilds;
using Misaki.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misaki.Discord
{
    public class Bot
    {
        private BotConfig _config;
        DiscordSocketConfig DiscotdConfig;
        List<Guild> Guilds;
        List<string> AudioList;
        private DiscordSocketClient _client;

        public Bot(BotConfig config)
        {
            _config = config;
        }
        public async Task Start()
        {
            DiscotdConfig = new DiscordSocketConfig { MessageCacheSize = 100 };
            _client = new DiscordSocketClient(DiscotdConfig);
            await _client.LoginAsync(TokenType.Bot, _config.token);
            await _client.StartAsync();
            _client.MessageUpdated += MessageUpdated;
            _client.Ready += async () =>
            {
                await Logger.Log("Bot is connected!", InfoSource.Discord);
                return;
            };

            await Task.Delay(-1);
        }
        private async Task Log(LogMessage msg)
        {
            switch (msg.Severity)
            {
                case LogSeverity.Info:
                    {
                        await Logger.Log(msg.ToString(prependTimestamp: false), InfoSource.Discord);
                        break;
                    }
                    case LogSeverity.Warning:
                    {
                        await Logger.Log(msg.ToString(prependTimestamp: false), InfoSource.Discord, InfoType.Warn);
                        break;
                    }
                case LogSeverity.Error:
                    {
                        await Logger.Log(msg.ToString(prependTimestamp: false), InfoSource.Discord, InfoType.Error);
                        break;
                    }
                case LogSeverity.Critical:
                    {
                        await Logger.Log(msg.ToString(prependTimestamp: false), InfoSource.Discord, InfoType.Error);
                        break;
                    }
            }
            return;
        }
        private static async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            // If the message was not in the cache, downloading it will result in getting a copy of `after`.
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after}");
        }
        private async Task CommandsHandler()
        {

        }




    }
}
