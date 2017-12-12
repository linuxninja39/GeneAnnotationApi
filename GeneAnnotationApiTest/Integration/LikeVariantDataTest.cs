using System.Collections.Generic;
using System.Linq;
using GeneAnnotationApi.Data;
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

    }
}