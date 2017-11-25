using System;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GeneAnnotationApiTest.Integration
{
    public class BaseDbTest : IDisposable
    {
        private readonly SqliteConnection _inMemorySqlite;
        protected readonly ILoggerFactory LoggerFactory;
        protected readonly GeneAnnotationDBContext Context;

        public BaseDbTest(bool initDb = true)
        {
            LoggerFactory = new LoggerFactory();
            LoggerFactory.AddConsole();
            LoggerFactory.AddDebug();

            if (initDb) Environment.SetEnvironmentVariable(InitializeConstants.GA_DB_RESET_VARIABLE_NAME, "1");
            var contextOptionsBuilder = new DbContextOptionsBuilder<GeneAnnotationDBContext>();
            contextOptionsBuilder.UseLoggerFactory(LoggerFactory);

            _inMemorySqlite = new SqliteConnection("Data Source=:memory:");
            _inMemorySqlite.Open();
            contextOptionsBuilder.UseSqlite(_inMemorySqlite);

            Context = new GeneAnnotationDBContext(contextOptionsBuilder.Options);

            Context.Database.OpenConnection();
            Context.Database.EnsureCreated();
            InitializeConstants.Initialize(Context);
        }

        public void Dispose()
        {
            _inMemorySqlite?.Dispose();
            LoggerFactory?.Dispose();
            Context?.Dispose();
        }
    }
}