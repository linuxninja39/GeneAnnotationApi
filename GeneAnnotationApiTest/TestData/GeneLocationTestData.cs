using GeneAnnotationApi.Entities;

namespace GeneAnnotationApiTest.TestData
{
    public class GeneLocationTestData
    {
        public static GeneLocation[] GeneLocations = {
            new GeneLocation{ Gene = GeneTestData.Genes[0], HgVersion = 19, Locus = "locus"}
        };
    }
}