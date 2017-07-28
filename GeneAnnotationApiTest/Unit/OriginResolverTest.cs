using System.Collections.Generic;
using AutoMapper;
using GeneAnnotationApi.AutoMapperProfiles.CustomResolvers;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;
using Xunit;

namespace GeneAnnotationApiTest.Unit
{
    public class OriginResolverTest
    {

        [Fact]
        public void GeneShouldProduceOriginTypes()
        {
            var originTypes = new OriginType[]
            {
                new OriginType{Name = "2112"},
                new OriginType{Name = "Entre Nous"},
            };
            var geneOriginTypes = new List<GeneOriginType>();
            var gene = new Gene();
            var geneOriginType = new GeneOriginType();
            geneOriginType.Gene = gene;
            geneOriginType.OriginType = originTypes[0];
            geneOriginTypes.Add(geneOriginType);
            geneOriginType = new GeneOriginType();
            geneOriginType.Gene = gene;
            geneOriginType.OriginType = originTypes[1];
            geneOriginTypes.Add(geneOriginType);

            gene.GeneOriginType = geneOriginTypes;

            var originResolver = new OriginResolver();
            var geneJsonModel = new GeneJsonModel();
            var originJsonModels = originResolver.Resolve(gene, geneJsonModel, geneJsonModel.Origin, null);
            
            Assert.Equal(originTypes[0].Name, originJsonModels[0].Name);
            Assert.Equal(originTypes[1].Name, originJsonModels[1].Name);
        }
    }
}