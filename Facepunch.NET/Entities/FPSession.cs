using Newtonsoft.Json;

namespace Facepunch.Entities
{
    public class FPSession : FPEntity
    {
        [JsonProperty("SessionId")]
        public int SessionId { get; set; }

        [JsonProperty("User")]
        public FPUser User { get; set; }
    }
}
