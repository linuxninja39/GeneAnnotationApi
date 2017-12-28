using GeneAnnotationApi.Data.Constants;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApiTest.TestData
{
    public static class GeneVariantTestData
    {
        public static GeneVariant[] GeneVariants = new[]
        {
            new GeneVariant
            {
                ZygosityType = ZygosityTypeConstants.ZygosityTypes[0],
                VariantType = VariantTypeConstants.VariantTypes[0].Children[0],
                Start = 1,
                End = 2
            },
            new GeneVariant
            {
                ZygosityType = ZygosityTypeConstants.ZygosityTypes[0],
                VariantType = VariantTypeConstants.VariantTypes[0].Children[0],
                Start = 1,
                End = 100
            },
            new GeneVariant
            {
                ZygosityType = ZygosityTypeConstants.ZygosityTypes[0],
                VariantType = VariantTypeConstants.VariantTypes[0].Children[0],
                Start = 20,
                End = 200
            },
            new GeneVariant
            {
                ZygosityType = ZygosityTypeConstants.ZygosityTypes[0],
                VariantType = VariantTypeConstants.VariantTypes[0].Children[0],
                Start = 300,
                End = 5000
            }
        };
    }
}