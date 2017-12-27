using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApiTest.TestData;

namespace GeneAnnotationApiTest.Integration
{
    public class GenesControllerTest : BaseControllerTest, IDisposable
    {
        public GenesControllerTest()
        {
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
            Context.Gene.Add(GeneTestData.Genes[0]);
            Context.GeneLocation.Add(GeneLocationTestData.GeneLocations[0]);
            Context.GeneCoordinate.Add(GeneCoordinateTestData.GeneCoordinates[0]);
            Context.SaveChanges();

            var res = await Client.GetAsync("/api/genes");
            Assert.True(res.StatusCode.Equals(HttpStatusCode.OK));
            var jsonString = await res.Content.ReadAsStringAsync();
            var geneDtos = JsonConvert
                .DeserializeObject<IList<GeneDto>>(jsonString);

            Assert.Equal(GeneTestData.Genes[0].KnownFunction, geneDtos[0].KnownFunction);
            Assert.Equal(GeneCoordinateTestData.GeneCoordinates[0].Start, geneDtos[0].GeneLocations[0].Start);
            Assert.Equal(GeneCoordinateTestData.GeneCoordinates[0].End, geneDtos[0].GeneLocations[0].End);
        }

        [Fact]
        public async Task SearchGenes()
        {
            Context.Gene.Add(GeneTestData.Genes[0]);
            Context.GeneLocation.Add(GeneLocationTestData.GeneLocations[0]);
            Context.GeneCoordinate.Add(GeneCoordinateTestData.GeneCoordinates[0]);
            Context.Symbol.Add(SymbolTestData.Symbols[0]);
            Context.SaveChanges();

            var res = await Client.GetAsync("/api/genes?globalFilter=" + SymbolTestData.Symbols[0].Name);
            Assert.True(res.StatusCode.Equals(HttpStatusCode.OK));
            var jsonString = await res.Content.ReadAsStringAsync();
            var geneDtos = JsonConvert
                .DeserializeObject<IList<GeneDto>>(jsonString);

            Assert.Equal(GeneTestData.Genes[0].KnownFunction, geneDtos[0].KnownFunction);
            Assert.Equal(GeneCoordinateTestData.GeneCoordinates[0].Start, geneDtos[0].GeneLocations[0].Start);
            Assert.Equal(GeneCoordinateTestData.GeneCoordinates[0].End, geneDtos[0].GeneLocations[0].End);
        }
    }
}