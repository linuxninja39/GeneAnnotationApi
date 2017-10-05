using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using GeneAnnotationApi.Data.Constants;
using GeneAnnotationApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneAnnotationApi.Data
{
    public static class InitializeConstants
    {
        public static readonly string GA_DB_RESET_VARIABLE_NAME = "GA_DB_RESET";

        private static readonly string DELETE_STRING = "DELETE FROM "
                                                       + "{0}";

        public static void Initialize(GeneAnnotationDBContext context)
        {
            var reset = Environment.GetEnvironmentVariable(GA_DB_RESET_VARIABLE_NAME);

            if (reset == null) return;
            InitCallTypes(context);
            InitOriginTypes(context);
            InitPathogenicSupportCategories(context);
            InitVariantTypes(context, null);
            InitZygosityTypes(context);
        }

        private static void InitCallTypes(GeneAnnotationDBContext context)
        {
            context.Database.ExecuteSqlCommand(string.Format(DELETE_STRING, "call_type"));

            foreach (var callType in CallTypeConstants.CallTypes)
            {
                context.CallType.Add(callType);
            }
            context.SaveChanges();
        }

        private static void InitOriginTypes(GeneAnnotationDBContext context)
        {
            BasicInit(context, "origin_type", OriginTypeConstants.OriginTypes);
        }
        
        private static void InitZygosityTypes(GeneAnnotationDBContext context)
        {
            BasicInit(context, "zygosity_type", ZygosityTypeConstants.ZygosityTypes);
        }
        
        private static void InitPathogenicSupportCategories(GeneAnnotationDBContext context)
        {
            BasicInit(
                context,
                "pathogenic_support_category",
                PathogenicSupportCategoryConstants.PathogenicSupportCategories
                );
        }

        private static void InitVariantTypes(GeneAnnotationDBContext context, VariantType[] variantTypes)
        {
            
            BasicInit(
                context,
                "variant_type",
                VariantTypeConstants.VariantTypes
                );
        }

        private static void BasicInit<T>(
            GeneAnnotationDBContext context,
            string tableName,
            IEnumerable<T> objects
            ) where T : class
        {
            
            context.Database.ExecuteSqlCommand(string.Format(DELETE_STRING, tableName));

            var passedType = typeof(T);
            var typeString = passedType.Name;

            foreach (var originType in objects)
            {
                var property = (DbSet<T>) context.GetType().GetProperty(typeString).GetValue(context);
                property.Add(originType);
//                (context.GetType().GetProperty(typeof(T).Name) as DbSet<T>).Add(originType);
            }
            context.SaveChanges();
        }
    }
}