using System.Text.Json.Serialization;

namespace WebSocket.Models
{
    public class Message
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        
        
        [JsonPropertyName("us")]
        public UserSelection Us {get; set; }
    }
}