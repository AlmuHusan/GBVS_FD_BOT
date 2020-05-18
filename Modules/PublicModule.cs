using System.IO;
using System.Threading.Tasks;
using Discord;
using System.Linq;
using Discord.Commands;
using GBVS_FD_BOT.Services;
using System.Collections.Generic;

namespace GBVS_FD_BOT.Modules
{
    // Modules must be public and inherit from an IModuleBase
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        // Dependency Injection will fill this value in for us
        public ImageService ImageService { get; set; }
        public FameDataService FrameDataService { get; set; }

        public MoveListService MoveListService { get; set; }

        Dictionary<string, string> charList = new Dictionary<string, string>(){

            { "gran","Gran" },
            { "ferry","Ferry" },
            { "katalina","Katalina" },
            {"metera","Metera" },
            {"lancelot","Lancelot" },
            {"percival","Percival" },
            {"zeta","Zeta" },
            {"djeeta","Djeeta" },
            {"soriz","Soriz" },
            {"lowain","Lowain" },
            {"ladiva","Ladiva" },
            {"narmaya","Narmaya" },
            {"zooey","Zooey" },
            {"vaseraga","Vaseraga" },
            {"charlotta","Charlotta" },
            {"beelzebub","Beelzebub" }
        };

        [Command("charlist")]
        public async Task CharListAsync()
        {
            var list = "(Input Value): (Character Name)\n";
            foreach (var key in charList.Keys)
            {
                list += key + ": " + charList[key]+"\n";
            }
            var builder = new EmbedBuilder();
            builder.WithTitle("List of available characters");
            builder.Description = list;
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("help")]
        public async Task Help()
        {
            var builder = new EmbedBuilder();
            builder.WithTitle("List of available commands:");
            builder.Description=
                "**-charlist:** Shows a list of all the characters available\n"+
                "**-movelist [charName]:** Shows a list of all the available moves based on the character inputed\n" +
                "**-fd [charName] [move]:** Displays the frame data based on the character and move entered";
                await Context.Channel.SendMessageAsync("", false, builder.Build());
        }   

        [Command("movelist")]
        public async Task MoveListAsync(string charName)
        {
            if (charList.Keys.Contains(charName))
            {
                var list = "";
                foreach (var k in MoveListService.MoveData[charName])
                {
                    list += k + ", ";
                }
                var builder = new EmbedBuilder();
                builder.WithTitle(charList[charName] + "'s movelist:");
                builder.Description = list;
                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
            else
            {
                await Context.Channel.SendMessageAsync($"Error bad input!");
            }
        }

        [Command("fd")]
        public async Task FrameDataAsync(string charName, string move)
        {

            if (!charList.ContainsKey(charName))
            {
                await ReplyAsync("This is not a valid character please try again.");
            }
            else if (!MoveListService.MoveData[charName].Contains(move))
            {
                await ReplyAsync("This is not a valid move please try again.");
            }
            else
            {
                var builder = new EmbedBuilder();
                var character = FrameDataService.FrameData[charList[charName]];
                var characterMove = character[move];
                builder.WithTitle(charList[charName]+" "+characterMove.move);
                builder.AddField("Damage", characterMove.damage, true);
                builder.AddField("Guard", characterMove.guard, true);
                builder.AddField("Startup", characterMove.startup, true);
                builder.AddField("Recovery", characterMove.recovery, true);
                builder.AddField("On Block", characterMove.onblock, true);
                builder.AddField("On Hit", characterMove.onhit, true);
                builder.WithThumbnailUrl(ImageService.ImageData[charName]);
                builder.WithColor(Color.Red);
                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
        }


    }
}
