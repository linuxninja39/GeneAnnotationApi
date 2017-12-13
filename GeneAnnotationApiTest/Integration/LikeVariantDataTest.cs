using System.Collections.Generic;
using System.Linq;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Data.Constants;
using GeneAnnotationApi.Entities;
using GeneAnnotationApiTest.TestData;
using Xunit;

namespace GeneAnnotationApiTest.Integration
{
    public class LikeVariantDataTest : BaseDbTest
    {
        public LikeVariantDataTest()
        {
            InitializeConstants.Initialize(Context);
        }

        [Fact]
        public void AddVariantTest()
        {
            var currentRow = new string[25];
            currentRow[LoadLikeData.ColVariantType] = "1";
            var likeVariantLoader = new LikeVariantData(Context, currentRow);
            likeVariantLoader.AddVariantType();

            Assert.NotNull(likeVariantLoader.CurrentVariant);
        }

        [Fact]
        public void AddZygosityTest()
        {
            var currentRow = new string[25];
            currentRow[LoadLikeData.ColZygosity] = "het";
            var likeVariantLoader = new LikeVariantData(Context, currentRow);

            likeVariantLoader.AddZygosity();

            Assert.Equal(
                ZygosityTypeConstants.ZygosityTypes[1].Name,
                likeVariantLoader.CurrentVariant.ZygosityType.Name
            );

            currentRow[LoadLikeData.ColZygosity] = "hom";
            likeVariantLoader = new LikeVariantData(Context, currentRow);

            likeVariantLoader.AddZygosity();

            Assert.Equal(
                ZygosityTypeConstants.ZygosityTypes[0].Name,
                likeVariantLoader.CurrentVariant.ZygosityType.Name
            );
        }
    }
}