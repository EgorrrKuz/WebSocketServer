using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebSocket.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebSocket.Middlewares
{
    public static class SendData
    {
        static Encoding _encoding = Encoding.UTF8;
        static RestClient _restClient = new RestClient();

        public static async Task Send(HttpContext context, System.Net.WebSockets.WebSocket webSocket)
        {
            var buffer = new byte[1024 * 8];
            
            var byteMessage = new byte[0];
            int countMessage = 0;

            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                var msg = JsonConvert.DeserializeObject<Message>(_encoding.GetString(buffer));
                
                if (msg.Type == "GetEngines")
                {
                    byteMessage = _encoding.GetBytes(JsonSerializer.Serialize(_restClient.Engines));
                    countMessage = JsonSerializer.Serialize(_restClient.Engines).Length;
                }
                else if (msg.Type == "GetMarkets")
                {
                    await _restClient.GetMarkets(msg.Us);
                    byteMessage = _encoding.GetBytes(JsonSerializer.Serialize(_restClient.Markets));
                    countMessage = JsonSerializer.Serialize(_restClient.Markets).Length;
                }
                else if (msg.Type == "GetBoards")
                {
                    await _restClient.GetBoards(msg.Us);
                    byteMessage = _encoding.GetBytes(JsonSerializer.Serialize(_restClient.Boards));
                    countMessage = JsonSerializer.Serialize(_restClient.Boards).Length;
                }
                else if (msg.Type == "GetSecurities")
                {
                    await _restClient.GetSecurities(msg.Us);
                    byteMessage = _encoding.GetBytes(JsonSerializer.Serialize(_restClient.Securities));
                    countMessage = JsonSerializer.Serialize(_restClient.Securities).Length;
                }
                
                await webSocket.SendAsync(new ArraySegment<byte>(byteMessage, 0, countMessage), result.MessageType, result.EndOfMessage, CancellationToken.None);
                
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}