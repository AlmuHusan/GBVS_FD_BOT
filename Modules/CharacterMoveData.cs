using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GBVS_FD_BOT.Modules
{
    public class CharacterMoveData
    {

        [JsonPropertyName("move")]
        public string move { get; set; }

        [JsonPropertyName("damage")]
        public string damage { get; set; }

        [JsonPropertyName("guard")]
        public string guard { get; set; }

        [JsonPropertyName("startup")]
        public string startup { get; set; }

        [JsonPropertyName("active")]
        public string active { get; set; }

        [JsonPropertyName("recovery")]
        public string recovery { get; set; }

        [JsonPropertyName("onblock")]
        public string onblock { get; set; }

        [JsonPropertyName("onhit")]
        public string onhit { get; set; }

        [JsonPropertyName("level")]
        public string level { get; set; }

        [JsonPropertyName("blockstun")]
        public string blockstun { get; set; }

        [JsonPropertyName("hitstun")]
        public string hitstun { get; set; }

        [JsonPropertyName("untech")]
        public string untech { get; set; }

        [JsonPropertyName("hitstop")]
        public string hitstop { get; set; }

        [JsonPropertyName("invul")]
        public string invul { get; set; }
    }
}
