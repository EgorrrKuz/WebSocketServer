using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebSocketServer.Handlers;
using WebSocketServer.SocketManager;

namespace WebSocketServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebSocketManager();
            services.AddTransient<WebSocketMessageHandler>();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseRouting();

            var webSocketOptions = new WebSocketOptions()
            {
                ReceiveBufferSize = 86 * 1024
            };
            
            webSocketOptions.AllowedOrigins.Add("http://localhost");
            webSocketOptions.AllowedOrigins.Add("https://localhost");

            app.UseWebSockets(webSocketOptions);
            
            
            // app.Use(async (context, next) =>
            // {
            //     if (context.Request.Path == "/data")
            //     {
            //         if (context.WebSockets.IsWebSocketRequest)
            //         {
            //             System.Net.WebSockets.WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            //             await SendData.Send(context, webSocket);
            //         }
            //         else
            //         {
            //             context.Response.StatusCode = 400;
            //         }
            //     }
            //     else
            //     {
            //         await next();
            //     }
            // });

            app.UseWebSockets();
            app.MapSockets("/ws", serviceProvider.GetService<WebSocketMessageHandler>());
            app.UseStaticFiles();
        }
    }
}