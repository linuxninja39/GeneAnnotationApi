using GeneAnnotationApi.Entities;

namespace GeneAnnotationApiTest.TestData
{
    public static class GeneTestData
    {
        public static Gene[] Genes = {
            new Gene { KnownFunction = "It's da greatest"},
            new Gene { KnownFunction = "Also da greatest"},
            new Gene { KnownFunction = "Love dat green hair"}
        };
    }
}