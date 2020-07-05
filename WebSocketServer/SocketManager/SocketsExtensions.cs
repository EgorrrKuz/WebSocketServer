using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WebSocketServer.Models;

namespace WebSocketServer.SocketManager
{
    public static class SocketsExtensions
    {
        public static List<UserSelection> Us = new List<UserSelection>();
        
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddTransient<ConnectionManager>();

            var exportedTypes = Assembly.GetEntryAssembly()?.ExportedTypes;
            if (exportedTypes != null)
                foreach (var type in exportedTypes)
                {
                    if (type.GetTypeInfo().BaseType == typeof(SocketHandler))
                        services.AddSingleton(type);
                }

            return services;
        }

        public static IApplicationBuilder MapSockets(this IApplicationBuilder app, PathString path,
            SocketHandler socket)
        {
            return app.Map(path, x => x.UseMiddleware<SocketMiddleware>(socket));
        }
    }
}