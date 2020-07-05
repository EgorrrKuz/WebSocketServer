using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebSocketServer.Models;

namespace WebSocketServer.Middlewares
{
    public static class RestClient
    {
        private static readonly HttpClient Client = new HttpClient();

        public static List<Engine> Engines = new List<Engine>();
        public static List<Market> Markets = new List<Market>();
        public static List<Board> Boards = new List<Board>();
        public static List<Security> Securities = new List<Security>();
        
        
        public static async Task GetEngines()
        {
            Engines.Clear();
            Client.DefaultRequestHeaders.Accept.Clear();

            var streamTask = Client.GetStreamAsync("http://localhost:5000/api/engines");
            var engines = await JsonSerializer.DeserializeAsync<List<Engine>>(await streamTask);

            foreach (var engine in engines)
            {
                Engines.Add(engine);
            }
        }
        
        public static async Task GetMarkets(UserSelection us)
        {
            Markets.Clear();
            Client.DefaultRequestHeaders.Accept.Clear();
        
            var streamTask = Client.GetStreamAsync("http://localhost:5000/api/markets");
            var markets = await JsonSerializer.DeserializeAsync<List<Market>>(await streamTask);
        
            foreach (var market in markets)
            {
                if (market.EngineName == us.Engine.Name)
                    Markets.Add(market);
            }
        }
        
        public static async Task GetBoards(UserSelection us)
        {
            Boards.Clear();
            Client.DefaultRequestHeaders.Accept.Clear();
        
            var streamTask = Client.GetStreamAsync("http://localhost:5000/api/boards");
            var boards = await JsonSerializer.DeserializeAsync<List<Board>>(await streamTask);
        
            foreach (var board in boards)
            {
                if (board.EngineName == us.Engine.Name && board.MarketName == us.Market.Name || us.Security != null && board.BoardId == us.Security.BoardId)
                    Boards.Add(board);
            }
        }
        
        public static async Task GetSecurities(UserSelection us)
        {
            Securities.Clear();
            Client.DefaultRequestHeaders.Accept.Clear();
        
            var streamTask = Client.GetStreamAsync("http://localhost:5000/api/securities");
            var securities = await JsonSerializer.DeserializeAsync<List<Security>>(await streamTask);
        
            foreach (var security in securities)
            {
                if (security.EngineName == us.Engine.Name && security.MarketName == us.Market.Name &&
                    security.BoardId == us.Board.BoardId)
                    Securities.Add(security);
            }
        }
    }
}