using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misaki.Utilities;

namespace Misaki.Discord.Services
{
    public class LoggingService
    {
        public LoggingService(DiscordSocketClient client, CommandService command)
        {
            client.Log += LogAsync;
            command.Log += LogAsync;
        }
        private async Task LogAsync(LogMessage message)
        {
            if (message.Exception is CommandException cmdException)
            {
                await Logger.Log($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}" + $" failed to execute in {cmdException.Context.Channel}.", InfoSource.Discord, InfoType.Error);
                await Logger.Log(cmdException.Message, InfoSource.Discord, InfoType.Error);
            }
            else await Logger.Log($"[General/{message.Severity}] {message}", InfoSource.Discord, InfoType.Info);

            return;
        }
    }
}
