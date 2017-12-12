using System;
using AutoMapper;
using GeneAnnotationApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.Repositories;
using GeneAnnotationApi.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GeneAnnotationApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            // Add framework services.
//            services.AddAutoMapper(typeof(Startup));
            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            ;
            
            AddRepositories(services);

            SetupDatabase(services);

        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IGeneRepository, GeneEfRepository>();
            services.AddScoped<IGeneCoordinateRepository, GeneCoordinateEfRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory
        )
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            app.UseCors(
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        ;
                }
            );

            app.UseMvc();
            app.UseStaticFiles();

            /*
            using (
                var serviceScope = app
                    .ApplicationServices
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope()
            )
            {
                var context = serviceScope.ServiceProvider.GetService<GeneAnnotationDBContext>();
                InitializeDatabase(context);
            }
            */
        }

        public virtual void SetupDatabase(IServiceCollection services)
        {
            //const string connection = @"Server=10.10.88.9;Database=GeneAnnotationDB;User=sa;Password=LGEN!2015";
            // local Server=localhost;Database=master;Trusted_Connection=True;
            var connection = Environment.GetEnvironmentVariable("GA_DB_CONNECTION_STRING");
            var dbEngine = Environment.GetEnvironmentVariable("GA_DB_ENGINE");

            if (dbEngine != null && dbEngine.Equals("postgres"))
            {
                services.AddDbContext<GeneAnnotationDBContext>(options => options.UseNpgsql(connection));
                return;
            }
            services.AddDbContext<GeneAnnotationDBContext>(options => options.UseSqlServer(connection));
        }
    }
}