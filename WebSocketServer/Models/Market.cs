using System.Text.Json.Serialization;

namespace WebSocketServer.Models
{
    public class Market
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("engineName")]
        public string EngineName { get; set; }
    }
}