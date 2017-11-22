using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GeneAnnotationApiTest.Integration
{
    public class InitializeDbTest : IDisposable
    {
        private readonly GeneAnnotationDBContext _context;

        public InitializeDbTest()
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
        }


        [Fact]
        public void InitializeContantsTest()
        {
            Environment.SetEnvironmentVariable(InitializeConstants.GA_DB_RESET_VARIABLE_NAME, "1");
            InitializeConstants.Initialize(_context);
            var gv = from r in _context.VariantType select r;
            var c = gv.Count();
            Assert.InRange(c, 1, 30);
        }

        [Fact]
        public void ResetDbTest()
        {
            var tableList = ResetDB.GetDbTableList();
            var contextProperties = _context.GetType().GetProperties();
            Assert.Equal(contextProperties.Length - 1, tableList.Count);
            
            ResetDB.ClearTables(_context);
            
            Assert.False(_context.Gene.Any());
        }

        public void Dispose()
        {
            _context?.Database.CloseConnection();
            _context?.Dispose();
        }
    }
}