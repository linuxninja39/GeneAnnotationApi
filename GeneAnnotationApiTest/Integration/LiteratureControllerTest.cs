using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AutoMapper;
using GeneAnnotationApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;
using GeneAnnotationApi.Dtos;

namespace GeneAnnotationApiTest.Integration
{
    public class LiteratureControllerTest
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }
        
        public LiteratureControllerTest()
        {
            // just to make sure mapper is initialzed
            _testServer = new TestServer(
                new WebHostBuilder()
                .UseStartup<Startup>()
                );
            Client = _testServer.CreateClient();
            Client.BaseAddress = new Uri("http://localhost");
        }
        
        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
        
        [Fact]
        public async void ShouldGetLiteratures()
        {
            var res = await Client.GetAsync("/api/literatures");
            Assert.True(res.StatusCode.Equals(HttpStatusCode.OK));
            var jsonString = await res.Content.ReadAsStringAsync();
            var literatureDtos = JsonConvert
                .DeserializeObject<List<LiteratureDto>>(jsonString);
            Assert.True(literatureDtos.Count == 2);
        }
    }
}