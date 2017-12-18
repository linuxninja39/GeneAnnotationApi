using System;
using System.Collections.Generic;
using System.Linq;
using GeneAnnotationApi.Data;
using GeneAnnotationApi.Data.Constants;
using GeneAnnotationApi.Entities;
using GeneAnnotationApiTest.TestData;
using Xunit;
using Xunit.Sdk;

namespace GeneAnnotationApiTest.Integration
{
    public class LikeLiteratureDataLoaderTest : BaseDbTest
    {
        public LikeLiteratureDataLoaderTest()
        {
            InitializeConstants.Initialize(Context);
        }

        [Fact]
        public void AddVariantTest()
        {
        }
    }
}