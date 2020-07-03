using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebSocket.Middlewares;

namespace WebSocket
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<RestClient>();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            var webSocketOptions = new WebSocketOptions();
            
            webSocketOptions.AllowedOrigins.Add("http://localhost");
            webSocketOptions.AllowedOrigins.Add("https://localhost");

            app.UseWebSockets(webSocketOptions);
            
            
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/data")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        System.Net.WebSockets.WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await SendData.Send(context, webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }
            });
        }
    }
}