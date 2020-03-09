using System.IO;
using System.Threading.Tasks;
using Discord;
using System.Linq;
using Discord.Commands;
using _02_commands_framework.Services;
using System.Collections.Generic;

namespace _02_commands_framework.Modules
{
    // Modules must be public and inherit from an IModuleBase
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        // Dependency Injection will fill this value in for us
        public PictureService PictureService { get; set; }
        Dictionary<string, string> charList = new Dictionary<string, string>(){

            { "ragna","Ragna_the_Bloodedge" },
            { "susan","Susanoo" },
            { "jin","Jin_Kisaragi" },
        };




        string[] moveList = {
            "j.236c","j.236a","j.236b","j.214b","j.214a","j.214c",
            "6p","4p","5p","236c","236c","214c","214c","214c","236c",
            "236b+c","214b+c","222b+c","5a","5aa","5aaa","5aaaa","4a","5b","5bb",
            "5bbb","5bbbb","5c","2c"
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
                builder.WithTitle(charList[charName]+": "+move);
                builder.AddField("Cost", "3",true);
                builder.AddField("HP", "665",true);
                builder.AddField("DPS", "42",true);
                builder.AddField("Hit Speed", "1.5sec",true);
                builder.AddField("SlowDown", "35%",true);
                builder.AddField("AOE", "63",true);
                builder.AddField("Hitstun", "23", true);
                builder.AddField("Invul", "-");
                builder.WithThumbnailUrl("https://www.avatarys.com/var/resizes/Cool-Avatars/Cartoons-Avatars/Super-Mario-Avatar-500x500.jpg?m=1455129118");

                builder.WithColor(Color.Red);
                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
        }


    }
}
