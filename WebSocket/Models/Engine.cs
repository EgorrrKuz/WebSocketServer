using System.Text.Json.Serialization;

namespace WebSocket.Models
{
    public class Engine
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}