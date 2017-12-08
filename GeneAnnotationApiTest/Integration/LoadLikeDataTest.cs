using System.Linq;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Entities;
using GeneAnnotationApiTest.TestData;
using Xunit;

namespace GeneAnnotationApiTest.Integration
{
    public class LoadLikeDataTest : BaseDbTest
    {
        private readonly Gene _insertedGeneWithSymbol;
        private readonly Gene _insertedGeneWithOutSymbol;

        public LoadLikeDataTest()
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
            var likeLoader = new LoadLikeData(Context, "like.csv.short");
            var ex = Record.Exception(
                () => { likeLoader.LoadFile(); }
            );

            Assert.Null(ex);
        }

        [Fact]
        public void FindOrCreateNewGene()
        {
            var likeLoader = new LoadLikeData(Context, "like.csv.short");
            likeLoader.CurrentRow = new[]
            {
                "bla",
                "ch1",
                "100",
                "200",
                "Bobs your ungle"
            };
            likeLoader.FindOrCreateGene();

            Assert.True(likeLoader.CurrentGene.Id > 0);
        }

        [Fact]
        public void FindOrCreateExistingGene()
        {
            var likeLoader = new LoadLikeData(Context, "like.csv.short");
            likeLoader.CurrentRow = new[]
            {
                "bla",
                "ch1",
                "100",
                "200",
                SymbolTestData.Symbols[0].Name
            };
            likeLoader.FindOrCreateGene();

            Assert.True(
                likeLoader.CurrentGene.Symbol.SingleOrDefault(s => s.Name == SymbolTestData.Symbols[0].Name) != null
            );
        }

        [Fact]
        public void AddLocation()
        {
            var likeLoader = new LoadLikeData(Context, "like.csv.short");
            const int start = 2112;
            const int end = 4224;
            likeLoader.CurrentRow = new[]
            {
                "stuffz",
                "ch13",
                start.ToString(),
                end.ToString(),
                "priest of syrinx"
            };
            likeLoader.CurrentGene = _insertedGeneWithOutSymbol;
            likeLoader.AddLocation();

            Assert.Equal(
                1,
                likeLoader
                    .CurrentGene
                    .GeneLocations.Single(gl => gl.HgVersion == 19)
                    .GeneCoordinates
                    .Count(gc => gc.Start == start && gc.End == end)
            );
        }
    }
}