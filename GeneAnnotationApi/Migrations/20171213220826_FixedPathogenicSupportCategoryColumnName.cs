using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Migrations
{
    public partial class FixedPathogenicSupportCategoryColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gene_variant_literature_pathogenic_support_category_pathogenic_support_category",
                table: "gene_variant_literature");

            migrationBuilder.RenameColumn(
                name: "pathogenic_support_category",
                table: "gene_variant_literature",
                newName: "pathogenic_support_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_gene_variant_literature_pathogenic_support_category",
                table: "gene_variant_literature",
                newName: "IX_gene_variant_literature_pathogenic_support_category_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "active_date",
                table: "name_synonym",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "annotation",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddForeignKey(
                name: "FK_gene_variant_literature_pathogenic_support_category_pathogenic_support_category_id",
                table: "gene_variant_literature",
                column: "pathogenic_support_category_id",
                principalTable: "pathogenic_support_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gene_variant_literature_pathogenic_support_category_pathogenic_support_category_id",
                table: "gene_variant_literature");

            migrationBuilder.RenameColumn(
                name: "pathogenic_support_category_id",
                table: "gene_variant_literature",
                newName: "pathogenic_support_category");

            migrationBuilder.RenameIndex(
                name: "IX_gene_variant_literature_pathogenic_support_category_id",
                table: "gene_variant_literature",
                newName: "IX_gene_variant_literature_pathogenic_support_category");

            migrationBuilder.AlterColumn<DateTime>(
                name: "active_date",
                table: "name_synonym",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "annotation",
                type: "datetime",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddForeignKey(
                name: "FK_gene_variant_literature_pathogenic_support_category_pathogenic_support_category",
                table: "gene_variant_literature",
                column: "pathogenic_support_category",
                principalTable: "pathogenic_support_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
