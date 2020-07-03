using System.Text.Json.Serialization;

namespace WebSocket.Models
{
    public class Board
    {
        [JsonPropertyName("boardGroupId")]
        public int BoardGroupId { get; set; }
        
        [JsonPropertyName("boardId")]
        public string BoardId { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("engineName")]
        public string EngineName { get; set; }
        
        [JsonPropertyName("marketName")]
        public string MarketName { get; set; }
    }
}