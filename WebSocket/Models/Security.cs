using System.Text.Json.Serialization;

namespace WebSocket.Models
{
    public class Security
    {
        [JsonPropertyName("secId")]
        public string SecId { get; set; }
        
        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }
        
        [JsonPropertyName("boardId")]
        public string BoardId { get; set; }
        
        [JsonPropertyName("marketName")]
        public string MarketName { get; set; }
        
        [JsonPropertyName("engineName")]
        public string EngineName { get; set; }
    }
}