using System;
using System.Net.Http;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace GeneAnnotationApiTest.Integration
{
    public abstract class BaseControllerTest
    {
        protected readonly TestServer _testServer;
        protected HttpClient Client { get; }

        public BaseControllerTest(bool initDb = true)
        {
            if (initDb) Environment.SetEnvironmentVariable(InitializeConstants.GA_DB_RESET_VARIABLE_NAME, "1");
            // just to make sure mapper is initialzed
            _testServer = new TestServer(
                new WebHostBuilder()
                    .UseEnvironment("Development")
                    .UseStartup<TestStartup>()
            );
            Client = _testServer.CreateClient();
            Client.BaseAddress = new Uri("http://localhost");
        }
    }
}