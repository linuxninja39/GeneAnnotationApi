using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Entities;
using GeneAnnotationApiTest.TestData;
using Microsoft.AspNetCore.JsonPatch.Internal;
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

            var names = new[]
            {
                "dat cool name",
                "tripartite motif-containing 49-like",
                "\"tripartite motif containing 49-like 1\"",
                "\"tripartite motif containing 49D2, pseudogene\""
            };
            var cells = new[]
            {
                "",
                "",
                names[0],
                "",
                "",
                "",
                "",
                string.Join(", ", names[1], names[2], names[3])
            };
            loadHugoData.addToGeneNames(gene, cells);
            Context.SaveChanges();
            var found = gene
                .GeneName
                .Sum(
                    geneName => names
                        .Select(name => name.Replace("\"", string.Empty))
                        .Count(quotelessName => geneName.Name.Equals(quotelessName))
                );

            Assert.True(found == 4);
        }

        [Fact]
        public void AddLocationTest()
        {
            var loadHugoData = new LoadHugoData(Context, "hugo.txt.short");

            var loci = new[]
            {
                "6",
                "12q24.31",
                "X",
                "Y",
                "3p24.1-p22.1"
            };

            var cells = new List<string>();

            for (var j = 0; j < loci.Length; j++)
            {
                var locus = loci[j];
                Context.Gene.Add(GeneTestData.Genes[j]);
                Context.SaveChanges();
                var gene = Context.Gene.Find(j + 1);
                for (var i = 0; i < LoadHugoData.ColChromosome + 1; i++)
                {
                    var cellValue = "";
                    if (i == LoadHugoData.ColChromosome)
                    {
                        cellValue = locus;
                    }

                    cells.Insert(i, cellValue);
                }
                loadHugoData.SaveLocation(gene, cells);
                Context.SaveChanges();
                var geneLocation = gene
                    .GeneLocations
                    .Where(x => x.Locus == loci[j])
                    .Where(gl => gl .Gene == gene)
                    .Single(gl => gl .HgVersion == 19)
                    ;
                var match = _parseHugoChromosome(locus);
                var chromosomeName = match.Groups[1].Value;
                Assert.Equal(geneLocation.Locus, locus);
                Assert.Equal(geneLocation.Chromosome.Name, chromosomeName);
            }

        }

        private Match _parseHugoChromosome(string chromosomeCellString)
        {
            return LoadHugoData.LocusRegex.Match(chromosomeCellString);
        }
    }
}