using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebSocketServer.Middlewares;
using WebSocketServer.Models;
using WebSocketServer.SocketManager;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebSocketServer.Handlers
{
    public class WebSocketMessageHandler : SocketHandler
    {
        public WebSocketMessageHandler(ConnectionManager connections) : base(connections)
        {
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);
            string socketId = Connections.GetId(socket);
            SocketsExtensions.Us.Add(new UserSelection(socketId, null, null, null, null));
        }

        public override async Task OnDisconnected(WebSocket socket)
        {
            await base.OnDisconnected(socket);
            string socketId = Connections.GetId(socket);

            foreach (var us in SocketsExtensions.Us)
            {
                if (us.Id == socketId)
                    SocketsExtensions.Us.Remove(us);
            }
        }

        public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            string socketId = Connections.GetId(socket);
            Message msg;

            try
            {
                msg = JsonConvert.DeserializeObject<Message>(Encoding.UTF8.GetString(buffer, 0, result.Count));
            }
            catch (JsonException)
            {
                msg = new Message();
            }

            string message = "";
            
            if (msg.Type == "GetEngines")
            {
                await RestClient.GetEngines();
                message = JsonSerializer.Serialize(RestClient.Engines);
            }

            await SendMessage(socketId, message);
        }
    }
}