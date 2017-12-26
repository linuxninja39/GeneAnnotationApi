using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Migrations
{
    public partial class VariantVariantTypeUniqueKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "uniq_start_end_codingchange",
                table: "gene_variant");

            migrationBuilder.CreateIndex(
                name: "uniq_start_end_codingchange",
                table: "gene_variant",
                columns: new[] { "start", "end", "variant_type_id", "coding_change" },
                unique: true,
                filter: "[coding_change] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "uniq_start_end_codingchange",
                table: "gene_variant");

            migrationBuilder.CreateIndex(
                name: "uniq_start_end_codingchange",
                table: "gene_variant",
                columns: new[] { "start", "end", "coding_change" },
                unique: true,
                filter: "[coding_change] IS NOT NULL");
        }
    }
}
