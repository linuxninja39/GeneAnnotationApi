using System;
using System.Net.Http;
using GeneAnnotationApi;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace GeneAnnotationApiTest.Integration
{
    public abstract class BaseControllerTest: IDisposable
    {
        protected readonly TestServer TestServer;
        protected HttpClient Client { get; }
        protected readonly GeneAnnotationDBContext Context;

        public BaseControllerTest(bool initDb = true)
        {
            if (initDb) Environment.SetEnvironmentVariable(InitializeConstants.GA_DB_RESET_VARIABLE_NAME, "1");
            // just to make sure mapper is initialzed
            TestServer = new TestServer(
                new WebHostBuilder()
                    .UseEnvironment("Development")
                    .UseStartup<TestStartup>()
            );
            Client = TestServer.CreateClient();
            Client.BaseAddress = new Uri("http://localhost");
            Context = TestServer.Host.Services.GetService(typeof(GeneAnnotationDBContext)) as GeneAnnotationDBContext;
            TestStartup.InitializeDatabase(Context);
        }
        
                public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }

    }
}