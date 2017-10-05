using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Data.Constants
{
    public class VariantTypeConstants
    {
        public static VariantType[] VariantTypes = new[]
        {
            new VariantType
            {
                Id = 1,
                Name = "SNV",
                Children = new[]
                {
                    new VariantType {Id = 2, Name = "Missense"},
                    new VariantType {Id = 3, Name = "Nonsense"},
                    new VariantType {Id = 4, Name = "Splice site"},
                    new VariantType {Id = 5, Name = "Frameshift"}
                }
            },
            new VariantType
            {
                Id = 6,
                Name = "Deletion",
                Children = new[]
                {
                    new VariantType {Id = 7, Name = "Whole"},
                    new VariantType
                    {
                        Name = "Partial",
                        Children = new[]
                        {
                            new VariantType {Id = 8, Name = "Intragenic"},
                            new VariantType {Id = 9, Name = "5'"},
                            new VariantType {Id = 10, Name = "3'"}
                        }
                    }
                }
            },
            new VariantType
            {
                Id = 11,
                Name = "Duplication",
                Children = new[]
                {
                    new VariantType {Id = 1012, Name = "Whole"},
                    new VariantType
                    {
                        Id = 1013, 
                        Name = "Partial",
                        Children = new VariantType[]
                        {
                            new VariantType {Id = 1014, Name = "Intragenic dkjf"},
                            new VariantType {Id = 1015, Name = "5'"},
                            new VariantType {Id = 1016, Name = "3'"}
                        }
                    }
                }
            }
        };
    }
}