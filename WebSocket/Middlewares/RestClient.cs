using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebSocket.Models;

namespace WebSocket.Middlewares
{
    public class RestClient
    {
        private static readonly HttpClient Client = new HttpClient();

        public List<Engine> Engines = new List<Engine>();
        public List<Market> Markets = new List<Market>();
        public List<Board> Boards = new List<Board>();
        public List<Security> Securities = new List<Security>();
        
        public async Task GetEngines()
        {
            Engines.Clear();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Add("User-Server", "Exchange data");
            
            var streamTask = Client.GetStreamAsync("http://localhost:5000/api/engines");
            var engines = await JsonSerializer.DeserializeAsync<List<Engine>>(await streamTask);

            foreach (var engine in engines)
            {
                Engines.Add(engine);
            }
        }
        
        public async Task GetMarkets(UserSelection us)
        {
            Markets.Clear();
            
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Add("User-Server", "Exchange data");
            
            var streamTask = Client.GetStreamAsync("http://localhost:5000/api/markets");
            var markets = await JsonSerializer.DeserializeAsync<List<Market>>(await streamTask);
        
            foreach (var market in markets)
            {
                if (market.EngineName == us.Engine)
                    Markets.Add(market);
            }
        }
        
        public async Task GetBoards(UserSelection us)
        {
            Boards.Clear();
            
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Add("User-Server", "Exchange data");
            
            var streamTask = Client.GetStreamAsync("http://localhost:5000/api/boards");
            var boards = await JsonSerializer.DeserializeAsync<List<Board>>(await streamTask);
        
            foreach (var board in boards)
            {
                if (board.EngineName == us.Engine && board.MarketName == us.Market)
                    Boards.Add(board);
            }
        }
        
        public async Task GetSecurities(UserSelection us)
        {
            Securities.Clear();
            
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Add("User-Server", "Exchange data");
            
            var streamTask = Client.GetStreamAsync("http://localhost:5000/api/securities");
            var securities = await JsonSerializer.DeserializeAsync<List<Security>>(await streamTask);
        
            foreach (var security in securities)
            {
                if (security.EngineName == us.Engine && security.MarketName == us.Market && security.BoardId == us.Board)
                    Securities.Add(security);
            }
        }
    }
}