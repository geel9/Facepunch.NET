using Newtonsoft.Json;

namespace Facepunch.Entities
{
    public class FPIcon : FPEntity
    {
        [JsonProperty("Data")]
        public string Data { get; set; }

        [JsonIgnore]
        public string Name { get; set; }
    }
}
