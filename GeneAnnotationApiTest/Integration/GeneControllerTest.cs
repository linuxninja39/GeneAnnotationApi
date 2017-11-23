using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApiTest.TestData;

namespace GeneAnnotationApiTest.Integration
{
    public class GeneControllerTest: BaseControllerTest, IDisposable
    {
       private readonly GeneAnnotationDBContext _context;

        public GeneControllerTest()
        {
            _context = _testServer.Host.Services.GetService(typeof(GeneAnnotationDBContext)) as GeneAnnotationDBContext;
            
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }

        [Fact]
        public async Task ShouldGet200()
        {
            var res = await Client.GetAsync("/api/genes");
            Assert.True(res.IsSuccessStatusCode);
        }

        [Fact]
        public async Task ShouldGetGenes()
        {
            _context.Gene.Add(GeneTestData.Genes[0]);
            _context.GeneLocation.Add(GeneLocationTestData.GeneLocations[0]);
            _context.SaveChanges();
            var res = await Client.GetAsync("/api/genes");
            Assert.True(res.StatusCode.Equals(HttpStatusCode.OK));
            var jsonString = await res.Content.ReadAsStringAsync();
            var geneDtos = JsonConvert
                .DeserializeObject<IList<GeneDto>>(jsonString);

            Assert.Equal(GeneTestData.Genes[0].KnownFunction, geneDtos[0].KnownFunction);
            Assert.True(true);
        }
    }
}