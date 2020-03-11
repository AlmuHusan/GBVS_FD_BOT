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
        public PictureService PictureService { get; set; }
        public CharacterMoveService CharacterMoveService { get; set; }
        Dictionary<string, string> charList = new Dictionary<string, string>(){

            { "gran","Gran" },
            { "ferry","Ferry" },
            { "katalina","Katalina" },
        };




        string[] moveList = {
            "j.236c","j.236a","j.236b","j.214b","j.214a","j.214c",
            "6p","4p","5p","236c","236c","214c","214c","214c","236c",
            "236b+c","214b+c","222b+c","5a","5aa","5aaa","5aaaa","4a","5b","5bb",
            "5bbb","5bbbb","5c","2c","c.L"
        };

        [Command("cat")]
        public async Task CatAsync()
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetCatPictureAsync();
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.png");
        }

        [Command("charlist")]
        public async Task CharListAsync()
        {
            var list = "(Value): (Character Name)\n";
            foreach (var key in charList.Keys)
            {
                list += key + ": " + charList[key]+"\n";
            }
            var builder = new EmbedBuilder();
            builder.WithTitle("List of available characters");
            builder.Description = list;
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("movelist")]
        public async Task MoveListAsync()
        {
            new CharacterMoveService();
            var list = "";
            foreach (var k in moveList)
            {
                list += k + "\n";
            }
            var builder = new EmbedBuilder();
            builder.WithTitle("List of available moves");
            builder.Description = list;
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("fd")]
        public async Task FrameDataAsync(string charName, string move)
        {
            if (!charList.ContainsKey(charName))
            {
                await ReplyAsync("This is not a valid character please try again.");
            }
            else if (!moveList.Contains(move))
            {
                await ReplyAsync("This is not a valid move please try again.");
            }
            else
            {
                var builder = new EmbedBuilder();
                var character = CharacterMoveService.FrameData["Gran"];
                var characterMove = character["c.L"];
                builder.WithTitle("Gran: "+characterMove.move);
                builder.AddField("Damage", characterMove.damage, true);
                builder.AddField("Guard", characterMove.guard, true);
                builder.AddField("Startup", characterMove.startup, true);
                builder.AddField("On Block", characterMove.onblock, true);
                builder.AddField("On Hit", characterMove.onhit, true);
                builder.AddField("OOGABOOGA", "-1 [+2] [[+7]]", true);
                builder.WithThumbnailUrl("https://www.avatarys.com/var/resizes/Cool-Avatars/Cartoons-Avatars/Super-Mario-Avatar-500x500.jpg?m=1455129118");

                builder.WithColor(Color.Red);
                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
        }


    }
}
