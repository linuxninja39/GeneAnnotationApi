using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using GeneAnnotationApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApiTest.TestData;
using Microsoft.Extensions.DependencyInjection;

namespace GeneAnnotationApiTest.Integration
{
    public class GeneControllerTest: IDisposable
    {
        private readonly TestServer _testServer;
        private HttpClient _client { get; }
        private GeneAnnotationDBContext _context;

        public GeneControllerTest()
        {
            _testServer = new TestServer(
                new WebHostBuilder()
                    .UseEnvironment("Development")
                    .UseStartup<TestStartup>()
            );
            _client = _testServer.CreateClient();
            _client.BaseAddress = new Uri("http://localhost");
            _context = _testServer.Host.Services.GetService(typeof(GeneAnnotationDBContext)) as GeneAnnotationDBContext;
            
        }

        public void Dispose()
        {
            _client.Dispose();
            _testServer.Dispose();
        }

        [Fact]
        public async Task ShouldGet200()
        {
            var res = await _client.GetAsync("/api/genes");
            Assert.True(res.IsSuccessStatusCode);
        }

        [Fact]
        public async Task ShouldGetGenes()
        {
            _context.Gene.Add(GeneTestData.Genes[0]);
            _context.GeneLocation.Add(GeneLocationTestData.GeneLocations[0]);
            _context.SaveChanges();
            var res = await _client.GetAsync("/api/genes");
            Assert.True(res.StatusCode.Equals(HttpStatusCode.OK));
            var jsonString = await res.Content.ReadAsStringAsync();
            var geneDtos = JsonConvert
                .DeserializeObject<IList<GeneDto>>(jsonString);

            Assert.Equal(GeneTestData.Genes[0].KnownFunction, geneDtos[0].KnownFunction);
            Assert.True(true);
        }
    }
}