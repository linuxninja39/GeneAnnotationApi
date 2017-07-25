using System;
using System.Linq;
using System.Net.Http.Headers;
using GeneAnnotationApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneAnnotationApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(GeneAnnotationDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Gene.Any())
            {
                return;
            }
            
            ClearTables(context);
            var humanGenomeAssembly = HumanGenomeAssemblyTable(context);
            var genes = GeneTable(context, humanGenomeAssembly);
            var zygosityTypes = ZygosityTypeTable(context);
            var variantTypes = VariantTypeTable(context);
            var callTypes = CallTypeTable(context);
            var geneVariants = GeneVariantTable(
                context,
                genes,
                zygosityTypes,
                variantTypes,
                callTypes
                );
        }

        private static void ClearTables(GeneAnnotationDBContext context)
        {
            var tableNames = new string[]
            {
                "gene_variant",
                "gene",
                "human_genome_assembly",
                "zygosity_type",
                "variant_type",
                "call_type"
            };
            foreach (var tableName in tableNames)
            {
                var sqlString = "DELETE FROM " + tableName;
                context.Database.ExecuteSqlCommand(sqlString);
            }
        }

        private static ZygosityType[] ZygosityTypeTable(GeneAnnotationDBContext context)
        {
            var zygosityTypes = new ZygosityType[]
            {
                new ZygosityType{Name = "Hetorzygous"},
                new ZygosityType{Name = "Homozygous"},
                new ZygosityType{Name = "Compound Heterozygous"}
            };
            foreach (var zygosityType in zygosityTypes)
            {
                context.ZygosityType.Add(zygosityType);
            }

            context.SaveChanges();

            return zygosityTypes;
        }

        private static VariantType[] VariantTypeTable(GeneAnnotationDBContext context)
        {
            var variantTypes = new VariantType[]
            {
                new VariantType{Name = "Deletion (whole gene)"},
                new VariantType{Name = "Partial Deletion (intragenic)"},
                new VariantType{Name = "Partial Deletion (deleted 5')"},
                new VariantType{Name = "Partial Deletion (deleted 3')"},
                new VariantType{Name = "Duplication (whole gene)"},
                new VariantType{Name = "Partial Duplication (intragenic)"},
                new VariantType{Name = "Partial Duplication (duplicated 5')"},
                new VariantType{Name = "Partial Duplication (duplicated 3')"},
                new VariantType{Name = "SNV, predicted lof"},
                new VariantType{Name = "SNV, predicted gof"},
                new VariantType{Name = "Splice site"},
                new VariantType{Name = "GWAS (within gene or nearest to this gene)"},
            };
            foreach (var variantType in variantTypes)
            {
                context.VariantType.Add(variantType);
            }

            context.SaveChanges();
            return variantTypes;
        }

        private static CallType[] CallTypeTable(GeneAnnotationDBContext context)
        {
            
            var callTypes = new CallType[]
            {
                new CallType{Name = "VOUS"},
                new CallType{Name = "Likely pathogenic"},
                new CallType{Name = "Pathogenic"},
                new CallType{Name = "Benign"},
                new CallType{Name = "autosomal recessive carrier"}
            };
            foreach (var callType in callTypes)
            {
                context.CallType.Add(callType);
            }
            
            context.SaveChanges();
            return callTypes;
        }

        private static HumanGenomeAssembly HumanGenomeAssemblyTable(GeneAnnotationDBContext context)
        {
            var humanGenomeAssembly = new HumanGenomeAssembly {Hg = 19};
            context.HumanGenomeAssembly.Add(humanGenomeAssembly);
            context.SaveChanges();

            return humanGenomeAssembly;
        }

        private static Gene[] GeneTable(GeneAnnotationDBContext context, HumanGenomeAssembly humanGenomeAssembly)
        {
            var genes = new Gene[]
            {
                new Gene
                {
                    GeneNameExpansion = "bob",
                    KnownGeneFunction = "cool function",
                    LastModifiedBy = "joe",
                    LastModifiedDate = DateTime.Now,
                    HumanGenomeAssembly = humanGenomeAssembly
                }
            };
            
            foreach (var gene in genes)
            {
                context.Gene.Add(gene);
            }
            context.SaveChanges();

            return genes;
        }

        private static GeneVariant[] GeneVariantTable(
            GeneAnnotationDBContext context,
            Gene[] genes,
            ZygosityType[] zygosityTypes,
            VariantType[] variantTypes,
            CallType[] callTypes
            )
        {
            var geneVariants = new GeneVariant[]
            {
                new GeneVariant
                {
                    Gene = genes[0],
                    ZygosityType = zygosityTypes[0],
                    VariantType = variantTypes[0],
                    CallType = callTypes[0]
                }
            };
            foreach (var geneVariant in geneVariants)
            {
                context.GeneVariant.Add(geneVariant);
            }
            
            context.SaveChanges();

            return geneVariants;
        }
    }
}