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
        public const string GA_DB_RESET_VARIABLE_NAME = "GA_DB_RESET";

        public static void Initialize(GeneAnnotationDBContext context)
        {
            var reset = Environment.GetEnvironmentVariable(GA_DB_RESET_VARIABLE_NAME);

            if (reset == null) return;
            if (!context.CallType.Any()) InitCallTypes(context);
            if (!context.OriginType.Any()) InitOriginTypes(context);
            if (!context.PathogenicSupportCategory.Any()) InitPathogenicSupportCategories(context);
            if (!context.VariantType.Any()) InitVariantTypes(context, null);
            if (!context.ZygosityType.Any()) InitZygosityTypes(context);
        }

        private static void InitCallTypes(GeneAnnotationDBContext context)
        {
            BasicInit(context, CallTypeConstants.CallTypes);
        }

        private static void InitOriginTypes(GeneAnnotationDBContext context)
        {
            BasicInit(context, OriginTypeConstants.OriginTypes);
        }
        
        private static void InitZygosityTypes(GeneAnnotationDBContext context)
        {
            BasicInit(context, ZygosityTypeConstants.ZygosityTypes);
        }
        
        private static void InitPathogenicSupportCategories(GeneAnnotationDBContext context)
        {
            BasicInit(
                context,
                PathogenicSupportCategoryConstants.PathogenicSupportCategories
                );
        }

        private static void InitVariantTypes(GeneAnnotationDBContext context, VariantType[] variantTypes)
        {
            
            BasicInit(
                context,
                VariantTypeConstants.VariantTypes
                );
        }

        private static void BasicInit<T>(
            GeneAnnotationDBContext context,
            IEnumerable<T> objects
            ) where T : class
        {
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