using System.Text.Json.Serialization;

namespace WebSocketServer.Models
{
    public class Message
    {
        /// <summary>
        /// Type of message. What you need to send or what you received
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
        
        /// <summary>
        /// Message object to be sent or received
        /// </summary>
        [JsonPropertyName("message")]
        public object Object {get; set; }
    }
}