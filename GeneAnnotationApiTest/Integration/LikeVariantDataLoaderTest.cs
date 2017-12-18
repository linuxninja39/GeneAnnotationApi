using System;
using System.Collections.Generic;
using System.Linq;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Data.Constants;
using GeneAnnotationApi.Entities;
using GeneAnnotationApiTest.TestData;
using Xunit;

namespace GeneAnnotationApiTest.Integration
{
    public class LikeVariantDataLoaderTest : BaseDbTest
    {
        public LikeVariantDataLoaderTest()
        {
            InitializeConstants.Initialize(Context);
        }

        [Fact]
        public void AddVariantTest()
        {
            var currentRow = new string[25];
            currentRow[LikeDataLoader.ColVariantType] = "1";
            var likeVariantLoader = new LikeVariantLoader(Context, currentRow);
            likeVariantLoader.AddVariantType();

            Assert.NotNull(likeVariantLoader.CurrentVariant);
        }

        [Fact]
        public void AddZygosityTest()
        {
            var currentRow = new string[25];
            currentRow[LikeDataLoader.ColZygosity] = "het";
            var likeVariantLoader = new LikeVariantLoader(Context, currentRow);

            likeVariantLoader.AddZygosity();

            Assert.Equal(
                ZygosityTypeConstants.ZygosityTypes[1].Name,
                likeVariantLoader.CurrentVariant.ZygosityType.Name
            );

            currentRow[LikeDataLoader.ColZygosity] = "hom";
            likeVariantLoader = new LikeVariantLoader(Context, currentRow);

            likeVariantLoader.AddZygosity();

            Assert.Equal(
                ZygosityTypeConstants.ZygosityTypes[0].Name,
                likeVariantLoader.CurrentVariant.ZygosityType.Name
            );
        }

        [Fact]
        public void AddCallTest()
        {
            var currentRow = new string[25];
            currentRow[LikeDataLoader.ColCall] = "VOUS";
            currentRow[LikeDataLoader.ColDateUpdated] = "08/30/2017 11:38:48";
            var likeVariantLoader = new LikeVariantLoader(Context, currentRow);

            likeVariantLoader.AddCall();

            Assert.True(
                likeVariantLoader.CurrentVariant.CallTypeGeneVariants.Count > 0
            );
        }

        [Fact]
        public void AddStartStopTest()
        {
            var currentRow = new string[25];
            currentRow[LikeDataLoader.ColStart] = "10";
            currentRow[LikeDataLoader.ColEnd] = "100";
            var likeVariantLoader = new LikeVariantLoader(Context, currentRow);

            likeVariantLoader.AddStartStop();

            Assert.Equal(
                currentRow[LikeDataLoader.ColStart],
                likeVariantLoader.CurrentVariant.Start.ToString()
            );
            Assert.Equal(
                currentRow[LikeDataLoader.ColEnd],
                likeVariantLoader.CurrentVariant.End.ToString()
            );
        }

        [Fact]
        public void DoImportTest()
        {
             var currentRow = new string[25];
            currentRow[LikeDataLoader.ColVariantType] = "1";
            currentRow[LikeDataLoader.ColStart] = "10";
            currentRow[LikeDataLoader.ColEnd] = "100";
            currentRow[LikeDataLoader.ColZygosity] = "het";
            currentRow[LikeDataLoader.ColCall] = "VOUS";
            currentRow[LikeDataLoader.ColDateUpdated] = "08/30/2017 11:38:48";
            var likeVariantLoader = new LikeVariantLoader(Context, currentRow);
            
            var ex = Record.Exception(
                () => { likeVariantLoader.DoImport(); }
            );

            Assert.Null(ex);
           
        }
    }
}