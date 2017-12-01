using System;
using System.Collections.Generic;
using GeneAnnotationApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneAnnotationApi.Data
{
    public static class ResetDB
    {
        public static void ClearTables(GeneAnnotationDBContext context)
        {
            foreach (var tableName in GetDbTableList(context))
            {
                var sqlString = "DELETE FROM "
                                + tableName
                                + "; "
                    ;
                context.Database.ExecuteSqlCommand(sqlString);
            }
        }

        public static IList<string> GetDbTableList(GeneAnnotationDBContext context)
        {
            
            var contextProperties = context.GetType().GetProperties();
            return new[]
            {
                "accession",
                "annotation",
                "annotation_author",
                "author_literature",
                "author",
                "annotation_literature",
                "annotation_gene_variant_literature",
                "gene_variant_literature",
                "literature",
                "annotation_gene",
                "annotation_gene_variant",
                "annotation",
                "symbol",
                "gene_location",
                "gene_origin_type",
                "origin_type",
                "app_user",
                "gene_variant",
                "gene_name",
                "gene_location",
                "gene",
                "human_genome_assembly",
                "zygosity_type",
                "variant_type",
                "call_type_gene_variant",
                "call_type",
                "disorder",
                "gene_var_lit_disorder",
                "chromosome",
                "name_synonym",
                "synonym",
                "pathogenic_support_category",
            };

        }
    }
}