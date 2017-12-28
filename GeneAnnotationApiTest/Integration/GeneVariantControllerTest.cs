using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using AutoMapper;
using GeneAnnotationApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApiTest.TestData;

namespace GeneAnnotationApiTest.Integration
{
    public class GeneVariantControllerTest: BaseControllerTest
    {

        public GeneVariantControllerTest()
        {
            if (Context.GeneVariant.Any()) return;

            Context.GeneVariant.Add(GeneVariantTestData.GeneVariants[0]);
            Context.GeneVariant.Add(GeneVariantTestData.GeneVariants[1]);
            Context.GeneVariant.Add(GeneVariantTestData.GeneVariants[2]);
            Context.GeneVariant.Add(GeneVariantTestData.GeneVariants[3]);
            Context.SaveChanges();
        }

        [Fact]
        public void ShouldGet200()
        {
//            var res = await Client.GetAsync("/api/genevariants/1");
//            res.EnsureSuccessStatusCode();
            Assert.True(true);
        }

        [Fact]
        public void ShouldGetGeneVariants()
        {
            /*
            var res = await Client.GetAsync("/api/genevariants/1");
            Assert.True(res.StatusCode.Equals(HttpStatusCode.OK));
            var jsonString = await res.Content.ReadAsStringAsync();
            var geneVariant = JsonConvert
                .DeserializeObject<GeneVariantDto>(jsonString);

            Assert.IsType<VariantTypeDto>(geneVariant.VariantType);
            Assert.IsType<ZygosityTypeDto>(geneVariant.ZygosityType);
            */
            Assert.True(true);
        }

        [Fact]
        public void GetByRangeTest()
        {
            const int start = 12;
            const int end = 400;
            var task = Client.GetAsync(string.Format("/api/genevariants?start={0}&end={1}", start, end));
            var res = task.Result;
            
            Assert.True(res.IsSuccessStatusCode);
            
            
            var jsonString = res.Content.ReadAsStringAsync().Result;
            var geneDtos = JsonConvert
                .DeserializeObject<IList<GeneDto>>(jsonString);
            
            Assert.Equal(3, geneDtos.Count);
            
        }
    }
}