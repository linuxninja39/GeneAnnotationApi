using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Migrations
{
    public partial class LocusNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "unique_origin_type_name",
                table: "origin_type");

            migrationBuilder.DropIndex(
                name: "uniq_start_end_codingchange",
                table: "gene_variant");

            migrationBuilder.DropIndex(
                name: "unique_call_type_name",
                table: "call_type");

            migrationBuilder.AlterColumn<string>(
                name: "locus",
                table: "gene_location",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)");

            migrationBuilder.CreateIndex(
                name: "unique_origin_type_name",
                table: "origin_type",
                column: "name",
                unique: true,
                filter: "[name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "uniq_start_end_codingchange",
                table: "gene_variant",
                columns: new[] { "start", "end", "coding_change" },
                unique: true,
                filter: "[coding_change] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "unique_call_type_name",
                table: "call_type",
                column: "name",
                unique: true,
                filter: "[name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "unique_origin_type_name",
                table: "origin_type");

            migrationBuilder.DropIndex(
                name: "uniq_start_end_codingchange",
                table: "gene_variant");

            migrationBuilder.DropIndex(
                name: "unique_call_type_name",
                table: "call_type");

            migrationBuilder.AlterColumn<string>(
                name: "locus",
                table: "gene_location",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "unique_origin_type_name",
                table: "origin_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uniq_start_end_codingchange",
                table: "gene_variant",
                columns: new[] { "start", "end", "coding_change" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "unique_call_type_name",
                table: "call_type",
                column: "name",
                unique: true);
        }
    }
}
