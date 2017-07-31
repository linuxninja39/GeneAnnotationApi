using System;
using System.Collections.Generic;
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
            var appUsers = AppUserTable(context);
            var chromosomes = ChromosomeTable(context);
            var genes = GeneTable(context, chromosomes);
            var geneLocations = GeneLocationTable(context, genes);
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
                "app_user",
                "gene_variant",
                "gene",
                "human_genome_assembly",
                "zygosity_type",
                "variant_type",
                "call_type",
                "chromosome"
            };
            foreach (var tableName in tableNames)
            {
                var sqlString = "DELETE FROM " + tableName;
                context.Database.ExecuteSqlCommand(sqlString);
            }
        }

        private static AppUser[] AppUserTable(GeneAnnotationDBContext context)
        {
            var appUsers = new AppUser[]
            {
                new AppUser{Name = "jacob"},
                new AppUser{Name = "john"},
                new AppUser{Name = "fred"}
            };
            
            foreach (var appUser in appUsers)
            {
                context.AppUser.Add(appUser);
            }
            
            context.SaveChanges();

            return appUsers;
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

        private static GeneLocation[] GeneLocationTable(GeneAnnotationDBContext context, Gene[] genes)
        {
            var geneLocations = new GeneLocation[]
            {
                new GeneLocation{Gene = genes[0], Chr = "1", Start = 444, End = 555, Locus = "locus", HgVersion = 19},
                new GeneLocation{Gene = genes[0], Chr = "1", Start = 446, End = 558, Locus = "locus", HgVersion = 38}
            };

            foreach (var geneLocation in geneLocations)
            {
                context.GeneLocation.Add(geneLocation);
            }

            context.SaveChanges();
            return geneLocations;
        }

        private static Gene[] GeneTable(
            GeneAnnotationDBContext context,
            Chromosome[] chromosomes
            )
        {
            var genes = new Gene[]
            {
                new Gene
                {
                    GeneNameExpansion = "bob",
                    KnownGeneFunction = "cool function",
                    LastModifiedBy = "joe",
                    LastModifiedDate = DateTime.Now,
                    Chromosome = chromosomes[0]
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

        private static Chromosome[] ChromosomeTable(GeneAnnotationDBContext context)
        {
            var chromosomeNames = new string[]
            {
                "1",
                "2",
                "3",
                "X",
                "Y"
            };

            var chromosomes = new List<Chromosome>();
            foreach (var chromosomeName in chromosomeNames)
            {
                var chromosome = new Chromosome {Name = chromosomeName};
                chromosomes.Add(chromosome);
                context.Chromosome.Add(chromosome);
            }
            context.SaveChanges();
            return chromosomes.ToArray();
        }
    }
    
}