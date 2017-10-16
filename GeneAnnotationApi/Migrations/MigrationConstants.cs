﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Routing;

namespace GeneAnnotationApi.Migrations
{
    public static class MigrationConstants
    {
        public const string INSERT_STRING_FORMAT = "INSERT INTO {0} ({1}) VALUES ({2})";

        public static string GetTableName(Type type)
        {
            var attribute = type.GetTypeInfo().GetCustomAttribute<TableAttribute>();
            return attribute.Name;
        }

        public static IEnumerable<string> GetColumnNames(Type type)
        {
            var ret = new List<string>();
            foreach (var propertyInfo in type.GetProperties())
            {
                var attribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();
                if (attribute != null)
                {
                    if (attribute.Name == "id")
                    {
                        continue;
                    }
                    ret.Add(attribute.Name);
                }
            }
            return ret;
        }
    }
}