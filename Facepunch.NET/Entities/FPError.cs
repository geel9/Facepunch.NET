using Newtonsoft.Json;

namespace Facepunch.Entities
{
    public class FPError
    {
        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
