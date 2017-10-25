using System;
using System.Linq;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace GeneAnnotationApiTest.Integration
{
    public class LoadHugoDataTest
    {
        private readonly GeneAnnotationDBContext _context;
        private readonly ILoggerFactory _loggerFactory;

        public LoadHugoDataTest()
        {
            try
            {
                var connectionStringBuilder = new SqliteConnectionStringBuilder {DataSource = ":memory:"};
                var connectionString = connectionStringBuilder.ToString();
                var connection = new SqliteConnection(connectionString);

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlite()
                    .AddDbContext<GeneAnnotationDBContext>(
                        options => options.UseSqlite(connection)
                    )
                    .BuildServiceProvider();

                _context = (GeneAnnotationDBContext) serviceProvider.GetService(typeof(GeneAnnotationDBContext));
                _context.Database.OpenConnection();
                _context.Database.EnsureCreated();
                
                _loggerFactory = (ILoggerFactory) serviceProvider.GetService(typeof(ILoggerFactory));
                _loggerFactory.AddConsole(LogLevel.Debug).AddDebug(LogLevel.Debug);
            }
            catch (Exception e)
            {
                var error = e.ToString();
                throw e;
            }
        }

        [Fact]
        public void LoadDataTest()
        {
            LoadHugoData.LoadData(_context, "hugo.txt.short");

            var logger = _loggerFactory.CreateLogger<LoadHugoDataTest>();
            
            logger.LogDebug("bla");

            Assert.Equal(19, _context.Gene.Count());
        }
    }
}