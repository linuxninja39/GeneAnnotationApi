using System;
using System.ComponentModel;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.Repositories;
using GeneAnnotationApi.Repositories.EntityFramework;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GeneAnnotationApiTest.Integration
{
    public class BaseRepositoryTest<T, U>: IDisposable where T : BaseEfRepository<U> where U : class
    {
        private readonly SqliteConnection _inMemorySqlite;
        protected readonly GeneAnnotationDBContext _context;
        protected readonly T _repository;
        protected readonly ILoggerFactory _loggerFactory;

        public BaseRepositoryTest(bool initDb = true)
        {
            
            _loggerFactory = new LoggerFactory();
            _loggerFactory.AddConsole();
            _loggerFactory.AddDebug();
            
            if (initDb) Environment.SetEnvironmentVariable(InitializeConstants.GA_DB_RESET_VARIABLE_NAME, "1");
            var contextOptionsBuilder = new DbContextOptionsBuilder<GeneAnnotationDBContext>();
            contextOptionsBuilder.UseLoggerFactory(_loggerFactory);
            
            _inMemorySqlite = new SqliteConnection("Data Source=:memory:");
            _inMemorySqlite.Open();
            contextOptionsBuilder.UseSqlite(_inMemorySqlite);
            
            _context = new GeneAnnotationDBContext(contextOptionsBuilder.Options);
            
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            InitializeConstants.Initialize(_context);
            
            _repository = (T)Activator.CreateInstance(typeof(T), _context);
            
        }

        public void Dispose()
        {
            _inMemorySqlite?.Dispose();
        }
    }
}