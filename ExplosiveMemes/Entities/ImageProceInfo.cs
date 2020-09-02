using Newtonsoft.Json;

namespace ExplosiveMemes.Entities
{
    public class ImageProceInfo
    {
        [JsonProperty("src")]
        public string Source { get; set; }

        [JsonProperty("dst")]
        public string Destination { get; set; }

        [JsonProperty("out")]
        public string Output { get; set; }
    }
}
