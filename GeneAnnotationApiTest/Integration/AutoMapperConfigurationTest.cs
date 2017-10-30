using AutoMapper;
using GeneAnnotationApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace GeneAnnotationApiTest.Integration
{
    public class AutoMapperConfigurationTest
    {
        
        public AutoMapperConfigurationTest()
        {
            // just to make sure mapper is initialzed
            var testServer = new TestServer(
                new WebHostBuilder()
                    .UseStartup<TestStartup>()
            );
        }
        
        [Fact]
        public void ConfigurationShouldBeValid()
        {
            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}