using GeneAnnotationApi;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GeneAnnotationApiTest.Integration
{
    public class TestStartup: Startup
    {
        public TestStartup(IHostingEnvironment env) : base(env)
        {
        }

        /// <inheritdoc />
        public override void InitializeDatabase(GeneAnnotationDBContext context)
        {
            //DbInitializer.Initialize(context);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            ResetDB.ClearTables(context);
            InitializeConstants.Initialize(context);
        }

        public override void SetupDatabase(IServiceCollection services)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = connectionStringBuilder.ToString();
            services.AddDbContext<GeneAnnotationDBContext>(options => options.UseSqlite(connection));
        }
    }
}