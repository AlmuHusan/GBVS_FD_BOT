using GBVS_FD_BOT.Modules;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GBVS_FD_BOT.Services
{
    public class FameDataService
    {
        string FrameDataDirectory = "../../Data/FrameData";
        public Dictionary<String, Dictionary<String,CharacterMoveData>> FrameData = new Dictionary<String, Dictionary<String, CharacterMoveData>>();
        public FameDataService()
        {
            foreach (string filePath in Directory.GetFiles(FrameDataDirectory, "*.json"))
            {
                string characterName = Path.GetFileNameWithoutExtension(filePath);
                using(StreamReader r = new StreamReader(filePath))
                {
                    Dictionary<String,CharacterMoveData> characterData = new Dictionary<String, CharacterMoveData>();
                    string json = r.ReadToEnd();
                    List<CharacterMoveData> moves = JsonConvert.DeserializeObject<List<CharacterMoveData>>(json);
                    foreach (CharacterMoveData move in moves)
                    {
                        characterData[move.move] = move;
                    }
                    FrameData[characterName] = characterData;
                }
                
            }
        }
    }
}
