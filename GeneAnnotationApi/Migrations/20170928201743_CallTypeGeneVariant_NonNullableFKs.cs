using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneAnnotationApi.Migrations
{
    public partial class CallTypeGeneVariant_NonNullableFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_call_type_gene_variant_call_type_call_type_id",
                table: "call_type_gene_variant");

            migrationBuilder.DropForeignKey(
                name: "FK_call_type_gene_variant_gene_variant_gene_variant_id",
                table: "call_type_gene_variant");

            migrationBuilder.AlterColumn<int>(
                name: "gene_variant_id",
                table: "call_type_gene_variant",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "call_type_id",
                table: "call_type_gene_variant",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_call_type_gene_variant_call_type_call_type_id",
                table: "call_type_gene_variant",
                column: "call_type_id",
                principalTable: "call_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_call_type_gene_variant_gene_variant_gene_variant_id",
                table: "call_type_gene_variant",
                column: "gene_variant_id",
                principalTable: "gene_variant",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_call_type_gene_variant_call_type_call_type_id",
                table: "call_type_gene_variant");

            migrationBuilder.DropForeignKey(
                name: "FK_call_type_gene_variant_gene_variant_gene_variant_id",
                table: "call_type_gene_variant");

            migrationBuilder.AlterColumn<int>(
                name: "gene_variant_id",
                table: "call_type_gene_variant",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "call_type_id",
                table: "call_type_gene_variant",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_call_type_gene_variant_call_type_call_type_id",
                table: "call_type_gene_variant",
                column: "call_type_id",
                principalTable: "call_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_call_type_gene_variant_gene_variant_gene_variant_id",
                table: "call_type_gene_variant",
                column: "gene_variant_id",
                principalTable: "gene_variant",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
