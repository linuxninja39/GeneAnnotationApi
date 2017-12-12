using GeneAnnotationApi.Entities;

namespace GeneAnnotationApiTest.TestData
{
    public static class GeneTestData
    {
        public static readonly Gene[] Genes = {
            new Gene { KnownFunction = "It's da greatest"},
            new Gene { KnownFunction = "Also da greatest"},
            new Gene { KnownFunction = "Love dat green hair"},
            new Gene { KnownFunction = "2112"},
            new Gene { KnownFunction = "Entre Nous"},
            new Gene { KnownFunction = "Music runs in the family"},
            new Gene { KnownFunction = "Kid Gloves"},
            new Gene {},
        };
    }
}