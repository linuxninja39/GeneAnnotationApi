using GeneAnnotationApi;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.Migrations;
using Xunit;

namespace GeneAnnotationApiTest.Unit
{
    public class MigrationConstantsTest
    {
        [Fact]
        public void GetTableNameTest()
        {
            var tableName = MigrationConstants.GetTableName(typeof(CallType));
            Assert.Equal("call_type", tableName);
        }

        [Fact]
        public void GetColumnNamesTest()
        {
            var columnNames = MigrationConstants.GetColumnNames(typeof(Gene));
        }
    }
}