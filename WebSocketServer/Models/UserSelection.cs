using System.Text.Json.Serialization;

namespace WebSocketServer.Models
{
    public class UserSelection
    {
        [JsonPropertyName("Id")]
        public string Id { get; private set; }
        
        [JsonPropertyName("engine")]
        public Engine Engine { get; private set; }
        
        [JsonPropertyName("market")]
        public Market Market{ get; private set; }
        
        [JsonPropertyName("board")]
        public Board Board{ get; private set; }
        
        [JsonPropertyName("security")]
        public Security Security{ get; private set; }

        public UserSelection(string id, Engine engine, Market market, Board board, Security security)
        {
            Id = id;
            Engine = engine;
            Market = market;
            Board = board;
            Security = security;
        }
    }
}