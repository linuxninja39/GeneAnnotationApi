using System;
using System.Linq;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Entities;
using GeneAnnotationApiTest.TestData;
using Microsoft.Extensions.Logging;
using Xunit;

namespace GeneAnnotationApiTest.Integration
{
    public class LoadHugoDataTest : BaseDbTest
    {
        [Fact]
        public void LoadDataTest()
        {
            var loadHugoData = new LoadHugoData(Context, "hugo.txt.short");
            loadHugoData.LoadData();

            var logger = LoggerFactory.CreateLogger<LoadHugoDataTest>();

            logger.LogDebug("bla");

            Assert.Equal(19, Context.Gene.Count());
        }

        [Fact]
        public void AddGeneNamesTest()
        {
            var loadHugoData = new LoadHugoData(Context, "hugo.txt.short");
            Context.Gene.Add(GeneTestData.Genes[0]);
            Context.GeneLocation.Add(GeneLocationTestData.GeneLocations[0]);
            Context.GeneCoordinate.Add(GeneCoordinateTestData.GeneCoordinates[0]);
            Context.SaveChanges();
            var gene = Context.Gene.Find(1);

            var cells = new[]
            {
                "",
                "",
                "dat cool name",
                "",
                "",
                "",
                "",
                "tripartite motif-containing 49-like, \"tripartite motif containing 49-like 1\", \"tripartite motif containing 49D2, pseudogene\""
            };
            loadHugoData.addToGeneNames(gene, cells);
            Context.SaveChanges();
        }
    }
}