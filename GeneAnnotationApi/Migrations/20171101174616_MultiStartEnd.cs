using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneAnnotationApi.Migrations
{
    public partial class MultiStartEnd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gene_variant_gene_GeneId",
                table: "gene_variant");

            migrationBuilder.DropForeignKey(
                name: "FK_synonym_gene_gene_id",
                table: "synonym");

            migrationBuilder.DropIndex(
                name: "IX_gene_variant_GeneId",
                table: "gene_variant");

            migrationBuilder.DropColumn(
                name: "GeneId",
                table: "gene_variant");

            migrationBuilder.DropColumn(
                name: "end",
                table: "gene_location");

            migrationBuilder.DropColumn(
                name: "start",
                table: "gene_location");

            migrationBuilder.DropColumn(
                name: "ensemble_id",
                table: "gene");

            migrationBuilder.DropColumn(
                name: "gene_name_expansion",
                table: "gene");

            migrationBuilder.DropColumn(
                name: "omim_id",
                table: "gene");

            migrationBuilder.DropColumn(
                name: "refseq",
                table: "gene");

            migrationBuilder.DropColumn(
                name: "ucsc",
                table: "gene");

            migrationBuilder.AlterColumn<int>(
                name: "gene_id",
                table: "synonym",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gene_name_expantion",
                table: "gene_name",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_synonym_gene_gene_id",
                table: "synonym",
                column: "gene_id",
                principalTable: "gene",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_synonym_gene_gene_id",
                table: "synonym");

            migrationBuilder.DropColumn(
                name: "gene_name_expantion",
                table: "gene_name");

            migrationBuilder.AlterColumn<int>(
                name: "gene_id",
                table: "synonym",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "GeneId",
                table: "gene_variant",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "end",
                table: "gene_location",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "start",
                table: "gene_location",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ensemble_id",
                table: "gene",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gene_name_expansion",
                table: "gene",
                type: "varchar(1000)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "omim_id",
                table: "gene",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "refseq",
                table: "gene",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ucsc",
                table: "gene",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_gene_variant_GeneId",
                table: "gene_variant",
                column: "GeneId");

            migrationBuilder.AddForeignKey(
                name: "FK_gene_variant_gene_GeneId",
                table: "gene_variant",
                column: "GeneId",
                principalTable: "gene",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_synonym_gene_gene_id",
                table: "synonym",
                column: "gene_id",
                principalTable: "gene",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
