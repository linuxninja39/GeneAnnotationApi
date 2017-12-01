using System.Linq;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Entities;
using GeneAnnotationApiTest.TestData;
using Xunit;

namespace GeneAnnotationApiTest.Integration
{
    public class LoadUcscDataTest : BaseDbTest
    {
        private readonly Gene _insertedGeneWithSymbol;
        private readonly Gene _insertedGeneWithOutSymbol;

        public LoadUcscDataTest()
        {
            _insertedGeneWithSymbol = GeneTestData.Genes[0];
            _insertedGeneWithOutSymbol = GeneTestData.Genes[1];
            Context.Gene.Add(_insertedGeneWithSymbol);
            Context.Gene.Add(_insertedGeneWithOutSymbol);
            Context.Symbol.Add(SymbolTestData.Symbols[0]);
            Context.SaveChanges();
        }

        [Fact]
        public void LoadFileTest()
        {
            var ucscLoader = new LoadUcscData(Context, "ucsc.txt.short");
            var ex = Record.Exception(
                () => { ucscLoader.LoadFile(); }
            );

            Assert.Null(ex);
        }

        [Fact]
        public void FindOrCreateNewGene()
        {
            var ucscLoader = new LoadUcscData(Context, "ucsc.txt.short");
            ucscLoader.CurrentRow = new[]
            {
                "bla",
                "ch1",
                "100",
                "200",
                "Bobs your ungle"
            };
            ucscLoader.FindOrCreateGene();

            Assert.True(ucscLoader.CurrentGene.Id > 0);
        }

        [Fact]
        public void FindOrCreateExistingGene()
        {
            var ucscLoader = new LoadUcscData(Context, "ucsc.txt.short");
            ucscLoader.CurrentRow = new[]
            {
                "bla",
                "ch1",
                "100",
                "200",
                SymbolTestData.Symbols[0].Name
            };
            ucscLoader.FindOrCreateGene();

            Assert.True(
                ucscLoader.CurrentGene.Symbol.SingleOrDefault(s => s.Name == SymbolTestData.Symbols[0].Name) != null
            );
        }

        [Fact]
        public void AddLocation()
        {
            var ucscLoader = new LoadUcscData(Context, "ucsc.txt.short");
            const int start = 2112;
            const int end = 4224;
            ucscLoader.CurrentRow = new[]
            {
                "stuffz",
                "ch13",
                start.ToString(),
                end.ToString(),
                "priest of syrinx"
            };
            ucscLoader.CurrentGene = _insertedGeneWithOutSymbol;
            ucscLoader.AddLocation();

            Assert.Equal(
                ucscLoader
                    .CurrentGene
                    .GeneLocations.Single(gl => gl.HgVersion == 19)
                    .GeneCoordinates
                    .Count(gc => gc.Start == start && gc.End == end),
                1
            );
        }
    }
}