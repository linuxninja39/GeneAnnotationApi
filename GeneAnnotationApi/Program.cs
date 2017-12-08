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
                StaticLoggerFactory.LoggerFactory = services.GetRequiredService<ILoggerFactory>();
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
            
            var loadHugo = Environment.GetEnvironmentVariable("GA_DB_LOAD_HUGO");
            if (loadHugo != null)
            {
                var hugoLoader = new LoadHugoData(context, "hugo.csv.short");
                hugoLoader.LoadData();
            }
            
            var loadUcsc = Environment.GetEnvironmentVariable("GA_DB_LOAD_UCSC");
            if (loadUcsc != null)
            {
                var ucscLoader = new LoadUcscData(context,"ucsc.csv.short");
                ucscLoader.LoadData();
            }
        }
    }
}