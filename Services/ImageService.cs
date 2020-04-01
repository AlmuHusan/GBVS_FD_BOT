using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GBVS_FD_BOT.Services
{
    public class ImageService
    {
        string MoveListDirectory = "../../Data/ImageData/ImageData.json";
        public Dictionary<String, String> ImageData;
        public ImageService()
        {
            using (StreamReader r = new StreamReader(MoveListDirectory))
            {
                string json = r.ReadToEnd();
                ImageData = JsonConvert.DeserializeObject<Dictionary<String,String>>(json);
            }

        }
    }
}
