using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Data.Constants
{
    public static class VariantTypeConstants
    {
        public static readonly VariantType[] VariantTypes = {
            new VariantType
            {
                Name = "SNV",
                Children = new[]
                {
                    new VariantType {Name = "Missense"},
                    new VariantType {Name = "Nonsense"},
                    new VariantType {Name = "Splice site"},
                    new VariantType {Name = "Frameshift"}
                }
            },
            new VariantType
            {
                
                Name = "Deletion",
                Children = new[]
                {
                    new VariantType {Name = "Whole"},
                    new VariantType
                    {
                        Name = "Partial",
                        Children = new[]
                        {
                            new VariantType {Name = "Intragenic"},
                            new VariantType {Name = "5'"},
                            new VariantType { Name = "3'"}
                        }
                    }
                }
            },
            new VariantType
            {
                
                Name = "Duplication",
                Children = new[]
                {
                    new VariantType {Name = "Whole"},
                    new VariantType
                    {
                        Name = "Partial",
                        Children = new[]
                        {
                            new VariantType {Name = "Intragenic"},
                            new VariantType {Name = "5'"},
                            new VariantType {Name = "3'"}
                        }
                    }
                }
            }
        };
    }
}