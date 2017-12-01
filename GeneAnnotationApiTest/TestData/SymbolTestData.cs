using GeneAnnotationApi.Entities;

namespace GeneAnnotationApiTest.TestData
{
    public class SymbolTestData
    {
        public static readonly Symbol[] Symbols =
        {
            new Symbol{Name = "Just between us", Gene = GeneTestData.Genes[0]}, 
        };
    }
}