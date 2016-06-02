using Newtonsoft.Json;

namespace Facepunch.Entities
{
    public class FPPost : FPEntity
    {
        [JsonProperty("PostId")]
        public int PostId { get; set; }

        [JsonProperty("UserId")]
        public int UserId { get; set; }

        [JsonProperty("Created")]
        public string Created { get; set; }

        [JsonProperty("Updated")]
        public string Updated { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Username")]
        public string Avatar { get; set; }
    }
}
