using Discord.Net;
using Discord;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Misaki.Utilities;
using Misaki.Discord.Resources.Guilds;
using Misaki.Resources;

namespace Misaki.Discord.Services
{
    internal class CommandsService
    {
        public CommandsService() { }
        public async Task BuildClientCommands(DiscordSocketClient client)
        {
            //status
            var skip = new SlashCommandBuilder()
                .WithName("skip")
                .WithDescription("Skip current song");
            var hi = new SlashCommandBuilder()
                .WithName("hi")
                .WithDescription("Say hi!");
            var play = new SlashCommandBuilder()
                .WithName("play")
                .WithDescription("Plays music from youtube")
                .AddOption("string", ApplicationCommandOptionType.String, "url", isRequired: true);
            var disconnect = new SlashCommandBuilder()
                .WithName("disconnect")
                .WithDescription("Leave voice channel");
            var pause = new SlashCommandBuilder() // later
                .WithName("pause")
                .WithDescription("pause / resume");
            var queue = new SlashCommandBuilder()
                .WithName("queue")
                .WithDescription("Dispays current audio queue");
            var loop = new SlashCommandBuilder()
                .WithName("loop")
                .WithDescription("Set loop mode")
                .AddOption(new SlashCommandOptionBuilder().WithName("loop-mode").WithDescription("Set loop mode").WithRequired(true)
                .AddChoice("None", "none")
                .AddChoice("single-repeat", "single")
                .AddChoice("multi-repeat", "multi")
                .WithType(ApplicationCommandOptionType.String)
                );
            try
            {
                await client.CreateGlobalApplicationCommandAsync(loop.Build());
                await client.CreateGlobalApplicationCommandAsync(skip.Build());
                await client.CreateGlobalApplicationCommandAsync(queue.Build());
                await client.CreateGlobalApplicationCommandAsync(hi.Build());
                await client.CreateGlobalApplicationCommandAsync(play.Build());
                await client.CreateGlobalApplicationCommandAsync(disconnect.Build());
            }
            catch (HttpException exception)
            {
                var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
                await Logger.Log(json, InfoSource.Discord, InfoType.Error);
            }

        }
        public async Task CommandHandler(SocketSlashCommand command)
        {
            //Search or create guild
            switch (command.Data.Name)
            {
                case "play":
                    {
                        // Get context: youtube / different source future?
                        //
                        // Get context: video or playlist

                        _ = Task.Run(async () =>
                        {

                            //await Play(cmd);
                        });
                        return;
                    }
                case "hi":
                    {
                        await command.RespondAsync("Hi" + command.User.Mention + "!");
                        break;
                    }
                case "queue": //fix queue
                    {
                        break;
                    }
                case "skip":
                    {
                        await command.RespondAsync("?");
                        //Play(cmd, forcenextaudio: true);
                        //Play(cmd, true);
                        break;
                    }
                case "loop":
                    {
                        string? mode = command.Data.Options.First().Value.ToString();
                        switch (mode)
                        {
                            case "none":
                                {
                                    //Guilds[G_ID].repeatMode = RepeatMode.None;
                                    break;
                                }
                            case "single":
                                {
                                    //Guilds[G_ID].repeatMode = RepeatMode.Single;
                                    break;
                                }
                            case "multi":
                                {
                                    //Guilds[G_ID].repeatMode = RepeatMode.Loop;
                                    break;
                                }

                        }
                        await command.RespondAsync("Loop mode set to " + mode);

                        break;
                    }
                case "disconnect":
                    {
                        /*
                        if (Guilds[G_ID].voiceChannel != null)
                        {
                            Guilds[G_ID].ClearQueue();
                            Guilds[G_ID].CloseStream();
                            cmd.RespondAsync("cya");
                            Guilds[G_ID].voiceChannel.DisconnectAsync(); //await not workin
                            Guilds[G_ID].voiceChannel = null;
                            Guilds[G_ID].inVoice = false;
                            Guilds[G_ID].Playing = false;
                        }*/
                        //else await command.RespondAsync("Not in voice channel ^_^");
                        break;
                    }
            }
        }
    }
}
