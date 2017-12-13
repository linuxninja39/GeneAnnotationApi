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

        [Fact]
        public void AddCallTest()
        {
            var currentRow = new string[25];
            currentRow[LoadLikeData.ColCall] = "VOUS";
            currentRow[LoadLikeData.ColDateUpdated] = "08/30/2017 11:38:48";
            var likeVariantLoader = new LikeVariantData(Context, currentRow);

            likeVariantLoader.AddCall();

            Assert.True(
                likeVariantLoader.CurrentVariant.CallTypeGeneVariants.Count > 0
            );
        }

        [Fact]
        public void AddStartStopTest()
        {
            var currentRow = new string[25];
            currentRow[LoadLikeData.ColStart] = "10";
            currentRow[LoadLikeData.ColEnd] = "100";
            var likeVariantLoader = new LikeVariantData(Context, currentRow);

            likeVariantLoader.AddStartStop();

            Assert.Equal(
                currentRow[LoadLikeData.ColStart],
                likeVariantLoader.CurrentVariant.Start.ToString()
            );
            Assert.Equal(
                currentRow[LoadLikeData.ColEnd],
                likeVariantLoader.CurrentVariant.End.ToString()
            );
        }

        [Fact]
        public void DoImportTest()
        {
             var currentRow = new string[25];
            currentRow[LoadLikeData.ColVariantType] = "1";
            currentRow[LoadLikeData.ColStart] = "10";
            currentRow[LoadLikeData.ColEnd] = "100";
            currentRow[LoadLikeData.ColZygosity] = "het";
            currentRow[LoadLikeData.ColCall] = "VOUS";
            currentRow[LoadLikeData.ColDateUpdated] = "08/30/2017 11:38:48";
            var likeVariantLoader = new LikeVariantData(Context, currentRow);
            
            var ex = Record.Exception(
                () => { likeVariantLoader.DoImport(); }
            );

            Assert.Null(ex);
           
        }
    }
}