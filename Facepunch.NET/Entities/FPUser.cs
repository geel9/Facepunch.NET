using Newtonsoft.Json;

namespace Facepunch.Entities
{
    public class FPUser : FPEntity
    {
        [JsonProperty("UserId")]
        public int UserId { get; set; }

        [JsonProperty("Avatar")]
        public string Avatar { get; set; }

        [JsonProperty("Usergroup")]
        public int Usergroup { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }
    }
}
