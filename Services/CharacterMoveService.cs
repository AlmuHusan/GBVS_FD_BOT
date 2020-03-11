using GBVS_FD_BOT.Modules;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GBVS_FD_BOT.Services
{
    public class CharacterMoveService
    {
        string dataDirectory = "../../CharacterMoveData";
        public Dictionary<String, Dictionary<String,CharacterMove>> FrameData = new Dictionary<String, Dictionary<String, CharacterMove>>();
        public CharacterMoveService()
        {
            foreach (string filePath in Directory.GetFiles(dataDirectory, "*.json"))
            {
                string characterName = Path.GetFileNameWithoutExtension(filePath);
                using(StreamReader r = new StreamReader(filePath))
                {
                    Dictionary<String,CharacterMove> characterData = new Dictionary<String, CharacterMove>();
                    string json = r.ReadToEnd();
                    List<CharacterMove> moves = JsonConvert.DeserializeObject<List<CharacterMove>>(json);
                    foreach (CharacterMove move in moves)
                    {
                        characterData[move.move] = move;
                    }
                    FrameData[characterName] = characterData;
                }
                
            }
            //foreach (var item in FrameData.Keys)
            //{
            //    var character = FrameData[item];
            //    Console.WriteLine(character["5A"].startup);
            //}

        }
    }
}
