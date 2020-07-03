using System.Text.Json.Serialization;

namespace WebSocket.Models
{
    public class UserSelection
    {
        [JsonPropertyName("engine")]
        public string Engine { get; set; }
        
        [JsonPropertyName("market")]
        public string Market{ get; set; }
        
        [JsonPropertyName("board")]
        public string Board{ get; set; }
        
        [JsonPropertyName("security")]
        public string Security{ get; set; }
    }
}