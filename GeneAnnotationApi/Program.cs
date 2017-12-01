using System;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GeneAnnotationApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<GeneAnnotationDBContext>();
                InitializeDatabase(context);

            } 


            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public static void InitializeDatabase(GeneAnnotationDBContext context)
        {
            context.Database.Migrate();
            InitializeConstants.Initialize(context);
        }
    }
}