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
        private readonly Gene _insertedGeneWithOutKnowFunction;

        public LoadLikeDataTest()
        {
            _insertedGeneWithSymbol = GeneTestData.Genes[0];
            _insertedGeneWithOutSymbol = GeneTestData.Genes[1];
            _insertedGeneWithOutKnowFunction = GeneTestData.Genes[7];
            Context.Gene.Add(_insertedGeneWithSymbol);
            Context.Gene.Add(_insertedGeneWithOutSymbol);
            Context.Gene.Add(_insertedGeneWithOutKnowFunction);
            Context.Symbol.Add(SymbolTestData.Symbols[0]);
            Context.SaveChanges();
        }

        [Fact]
        public void LoadFileTest()
        {
            var likeLoader = new LikeDataLoader(Context, "like.csv.short");
            var ex = Record.Exception(
                () => { likeLoader.LoadFile(); }
            );

            Assert.Null(ex);
        }

        [Fact]
        public void FindOrCreateNewGene()
        {
            var likeLoader = new LikeDataLoader(Context, "like.csv.short");
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
            var likeLoader = new LikeDataLoader(Context, "like.csv.short");
            likeLoader.CurrentRow = new[]
            {
                SymbolTestData.Symbols[0].Name,
                "ch1",
                "100",
                "200",
                "locus",
            };
            likeLoader.FindOrCreateGene();

            Assert.True(
                likeLoader.CurrentGene.Symbol.SingleOrDefault(s => s.Name == SymbolTestData.Symbols[0].Name) != null
            );
        }

        [Fact]
        public void AddLocation()
        {
            var likeLoader = new LikeDataLoader(Context, "like.csv.short");
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

            Assert.Equal(
                likeLoader.CurrentRow[LikeDataLoader.ColLocus],
                likeLoader
                    .CurrentGene
                    .GeneLocations.Single(gl => gl.HgVersion == 19)
                    .Locus
            );
        }

        [Fact]
        public void AddKnownFunctionTest()
        {
            var likeLoader = new LikeDataLoader(Context, "like.csv.short");
            likeLoader.CurrentRow = new[]
            {
                "stuffz",
                "ch13",
                "1",
                "2",
                "priest of syrinx",
                "",
                "",
                "",
                "",
                "God Of Balance"
            };
            likeLoader.CurrentGene = _insertedGeneWithOutKnowFunction;
            likeLoader.AddKnownFunction();

            Assert.Equal(
                likeLoader.CurrentRow[LikeDataLoader.ColKnownFunction],
                likeLoader
                    .CurrentGene
                    .KnownFunction
            );
        }
    }
}