using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using AutoMapper;
using GeneAnnotationApi;
using GeneAnnotationApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApiTest.Integration
{
    public class LiteratureControllerTest: BaseControllerTest, IDisposable
    {

        public LiteratureControllerTest(): base(false)
        {
            var context = TestServer.Host.Services.GetService(typeof(GeneAnnotationDBContext)) as GeneAnnotationDBContext;
            if (context == null) return;
            context.Literature.Add(
                new Literature {Url = "http://lit1", Title = "lit1"}
            );
            context.Literature.Add(
                new Literature {Url = "http://lit2", Title = "lit2"}
            );
            context.SaveChanges();
            var lits = context.Literature.ToList();
        }
        
        [Fact]
        public async void ShouldGetLiteratures()
        {
            var context = TestServer.Host.Services.GetService(typeof(GeneAnnotationDBContext)) as GeneAnnotationDBContext;
            var lits = context.Literature.ToList();
            var res = await Client.GetAsync("/api/literatures");
            Assert.True(res.StatusCode.Equals(HttpStatusCode.OK));
            var jsonString = await res.Content.ReadAsStringAsync();
            var literatureDtos = JsonConvert
                .DeserializeObject<List<LiteratureDto>>(jsonString);
            Assert.Equal(2,literatureDtos.Count);
        }
    }
}