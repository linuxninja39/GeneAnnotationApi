using AutoMapper;
using GeneAnnotationApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace GeneAnnotationApiTest.Integration
{
    public class AutoMapperConfigurationTest
    {
        private readonly TestServer _testServer;
        
        public AutoMapperConfigurationTest()
        {
            // just to make sure mapper is initialzed
            _testServer = new TestServer(
                new WebHostBuilder()
                .UseStartup<Startup>()
                );
        }
        
        [Fact]
        public void ConfigurationShouldBeValid()
        {
            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}