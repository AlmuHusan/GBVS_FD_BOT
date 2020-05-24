using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GBVS_FD_BOT.Services
{
    public class MoveListService
    {
        string MoveListDirectory = "../../Data/MoveListData/CharMoveList.json";
        public Dictionary<String, List<String>> MoveData;
        public MoveListService()
        {
            using (StreamReader r = new StreamReader(MoveListDirectory))
            {
                string json = r.ReadToEnd();
                MoveData = JsonConvert.DeserializeObject<Dictionary<String, List<String>>>(json);
            }

        }
    }
}
