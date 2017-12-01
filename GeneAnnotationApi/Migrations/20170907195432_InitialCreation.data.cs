using System.Collections.Generic;
using System.Linq;
using GeneAnnotationApi.Data.Constants;
using GeneAnnotationApi.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneAnnotationApi.Migrations
{
    public static class InitialCreationData
    {
        public static void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var sqlString in CallTypeStatements())
            {
                migrationBuilder.Sql(sqlString);
            }

            foreach (var sqlString in OriginTypeStatements())
            {
                migrationBuilder.Sql(sqlString);
            }
        }

        public static void Down(MigrationBuilder migrationBuilder)
        {
        }

        private static IEnumerable<string> CallTypeStatements()
        {
            var columns = MigrationConstants.GetColumnNames(typeof(CallType));
            return CallTypeConstants
                .CallTypes
                .Select(
                    callType => string.Format(
                        MigrationConstants.InsertStringFormat,
                        MigrationConstants.GetTableName(typeof(CallType)),
                        string.Join(",", columns),
                        string.Join(",", "'" + callType.Name + "'")
                    )
                )
                .ToList();
        }

        private static IEnumerable<string> OriginTypeStatements()
        {
            var columns = MigrationConstants.GetColumnNames(typeof(OriginType));
            return OriginTypeConstants
                .OriginTypes
                .Select(
                    callType => string.Format(
                        MigrationConstants.InsertStringFormat,
                        MigrationConstants.GetTableName(typeof(OriginType)),
                        string.Join(",", columns),
                        string.Join(",", "'" + callType.Name + "'")
                    )
                )
                .ToList();
        }
    }
}