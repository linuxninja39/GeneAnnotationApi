using System;
using System.Reflection;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApiTest.TestData
{
    public static class InitTestData
    {
        public static void FillDb(GeneAnnotationDBContext context)
        {
            var properties = context.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var property = propertyInfo.GetValue(context);
                var testDataClassName = propertyInfo.Name + "TestData";
                var type = Type.GetType(testDataClassName);
                var myObject = Activator.CreateInstance(type);
            }
        }
    }
}