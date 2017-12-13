using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Data.Constants
{
    public class ZygosityTypeConstants
    {

        public static readonly ZygosityType[] ZygosityTypes = {
            new ZygosityType{Name = "Homozygous"},
            new ZygosityType{Name = "Heterozygous"},
            new ZygosityType{Name = "Hemizygous"},
            new ZygosityType{Name = "Nullizygous"}
        };
    }
}