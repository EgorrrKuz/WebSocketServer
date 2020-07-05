using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebSocketServer.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebSocketServer.Middlewares
{
    public static class SendData
    {
        static Encoding _encoding = Encoding.UTF8;

        public static async Task Send(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 86];
            
            var byteMessage = new byte[0];
            int countMessage = 0;

            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                var msg = JsonConvert.DeserializeObject<Message>(_encoding.GetString(buffer));

                if (msg.Type == "GetEngines")
                {
                    await RestClient.GetEngines();
                    byteMessage = _encoding.GetBytes(JsonSerializer.Serialize(RestClient.Engines));
                    countMessage = JsonSerializer.Serialize(RestClient.Engines).Length;
                }
                // else if (msg.Type == "GetMarkets")
                // {
                //     await RestClient.GetMarkets(msg.Us);
                //     byteMessage = _encoding.GetBytes(JsonSerializer.Serialize(RestClient.Markets));
                //     countMessage = JsonSerializer.Serialize(RestClient.Markets).Length;
                // }
                // else if (msg.Type == "GetBoards")
                // {
                //     await RestClient.GetBoards(msg.Us);
                //     byteMessage = _encoding.GetBytes(JsonSerializer.Serialize(RestClient.Boards));
                //     countMessage = JsonSerializer.Serialize(RestClient.Boards).Length;
                // }
                // else if (msg.Type == "GetSecurities")
                // {
                //     await RestClient.GetSecurities(msg.Us);
                //     byteMessage = _encoding.GetBytes(JsonSerializer.Serialize(RestClient.Securities));
                //     countMessage = JsonSerializer.Serialize(RestClient.Securities).Length;
                // }
                // else if (msg.Type == "GetPrediction")
                // {
                //     byteMessage = buffer;
                //     countMessage = buffer.Length;
                // }
                
                await webSocket.SendAsync(new ArraySegment<byte>(byteMessage, 0, countMessage), result.MessageType, result.EndOfMessage, CancellationToken.None);
                
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}