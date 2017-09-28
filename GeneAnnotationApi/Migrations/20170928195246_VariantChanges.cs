using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GeneAnnotationApi.Migrations
{
    public partial class VariantChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gene_chromosome_chromosome_id",
                table: "gene");

            migrationBuilder.DropForeignKey(
                name: "FK_gene_variant_call_type_call_type_id",
                table: "gene_variant");

            migrationBuilder.DropForeignKey(
                name: "FK_gene_variant_gene_gene_id",
                table: "gene_variant");

            migrationBuilder.DropForeignKey(
                name: "FK_gene_variant_literature_literature_literature_id",
                table: "gene_variant_literature");

            migrationBuilder.DropTable(
                name: "alternate_gene_name");

            migrationBuilder.DropIndex(
                name: "IX_gene_variant_call_type_id",
                table: "gene_variant");

            migrationBuilder.DropIndex(
                name: "uniq_gene_zygosity_variant",
                table: "gene_variant");

            migrationBuilder.DropIndex(
                name: "IX_gene_chromosome_id",
                table: "gene");

            migrationBuilder.DropColumn(
                name: "supports_pathogenicity",
                table: "gene_variant_literature");

            migrationBuilder.DropColumn(
                name: "chr",
                table: "gene_location");

            migrationBuilder.DropColumn(
                name: "chromosome_id",
                table: "gene");

            migrationBuilder.DropColumn(
                name: "last_modified_by",
                table: "gene");

            migrationBuilder.DropColumn(
                name: "last_modified_date",
                table: "gene");

            migrationBuilder.RenameColumn(
                name: "gene_id",
                table: "gene_variant",
                newName: "GeneId");

            migrationBuilder.RenameColumn(
                name: "call_type_id",
                table: "gene_variant",
                newName: "start");

            migrationBuilder.RenameColumn(
                name: "known_gene_function",
                table: "gene",
                newName: "known_function");

            migrationBuilder.RenameColumn(
                name: "annotation",
                table: "annotation",
                newName: "note");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "variant_type",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "parent_id",
                table: "variant_type",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "active_date",
                table: "symbol",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pubMedId",
                table: "literature",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "literature_id",
                table: "gene_variant_literature",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "added_at",
                table: "gene_variant_literature",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "app_user_id",
                table: "gene_variant_literature",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pathogenic_support_category",
                table: "gene_variant_literature",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "GeneId",
                table: "gene_variant",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "coding_change",
                table: "gene_variant",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "end",
                table: "gene_variant",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "chromosome_id",
                table: "gene_location",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GeneLocationId",
                table: "gene_location",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "call_type_gene_variant",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    active_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    call_type_id = table.Column<int>(nullable: true),
                    gene_variant_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_call_type_gene_variant", x => x.id);
                    table.ForeignKey(
                        name: "FK_call_type_gene_variant_call_type_call_type_id",
                        column: x => x.call_type_id,
                        principalTable: "call_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_call_type_gene_variant_gene_variant_gene_variant_id",
                        column: x => x.gene_variant_id,
                        principalTable: "gene_variant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pathogenic_support_category",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pathogenic_support_category", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_variant_type_parent_id",
                table: "variant_type",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "unique_origin_type_name",
                table: "origin_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gene_variant_literature_app_user_id",
                table: "gene_variant_literature",
                column: "app_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_gene_variant_literature_pathogenic_support_category",
                table: "gene_variant_literature",
                column: "pathogenic_support_category");

            migrationBuilder.CreateIndex(
                name: "IX_gene_variant_GeneId",
                table: "gene_variant",
                column: "GeneId");

            migrationBuilder.CreateIndex(
                name: "uniq_start_end_codingchange",
                table: "gene_variant",
                columns: new[] { "start", "end", "coding_change" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gene_location_GeneLocationId",
                table: "gene_location",
                column: "GeneLocationId");

            migrationBuilder.CreateIndex(
                name: "unique_hg_version_gene",
                table: "gene_location",
                columns: new[] { "hg_version", "gene_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "unique_call_type_name",
                table: "call_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_call_type_gene_variant_gene_variant_id",
                table: "call_type_gene_variant",
                column: "gene_variant_id");

            migrationBuilder.CreateIndex(
                name: "unique_call_type_gene_variant",
                table: "call_type_gene_variant",
                columns: new[] { "call_type_id", "gene_variant_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "unique_category_name",
                table: "pathogenic_support_category",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_gene_location_chromosome_GeneLocationId",
                table: "gene_location",
                column: "GeneLocationId",
                principalTable: "chromosome",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_gene_variant_gene_GeneId",
                table: "gene_variant",
                column: "GeneId",
                principalTable: "gene",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_gene_variant_literature_app_user_app_user_id",
                table: "gene_variant_literature",
                column: "app_user_id",
                principalTable: "app_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gene_variant_literature_literature_literature_id",
                table: "gene_variant_literature",
                column: "literature_id",
                principalTable: "literature",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gene_variant_literature_pathogenic_support_category_pathogenic_support_category",
                table: "gene_variant_literature",
                column: "pathogenic_support_category",
                principalTable: "pathogenic_support_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_variant_type_variant_type_parent_id",
                table: "variant_type",
                column: "parent_id",
                principalTable: "variant_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gene_location_chromosome_GeneLocationId",
                table: "gene_location");

            migrationBuilder.DropForeignKey(
                name: "FK_gene_variant_gene_GeneId",
                table: "gene_variant");

            migrationBuilder.DropForeignKey(
                name: "FK_gene_variant_literature_app_user_app_user_id",
                table: "gene_variant_literature");

            migrationBuilder.DropForeignKey(
                name: "FK_gene_variant_literature_literature_literature_id",
                table: "gene_variant_literature");

            migrationBuilder.DropForeignKey(
                name: "FK_gene_variant_literature_pathogenic_support_category_pathogenic_support_category",
                table: "gene_variant_literature");

            migrationBuilder.DropForeignKey(
                name: "FK_variant_type_variant_type_parent_id",
                table: "variant_type");

            migrationBuilder.DropTable(
                name: "call_type_gene_variant");

            migrationBuilder.DropTable(
                name: "pathogenic_support_category");

            migrationBuilder.DropIndex(
                name: "IX_variant_type_parent_id",
                table: "variant_type");

            migrationBuilder.DropIndex(
                name: "unique_origin_type_name",
                table: "origin_type");

            migrationBuilder.DropIndex(
                name: "IX_gene_variant_literature_app_user_id",
                table: "gene_variant_literature");

            migrationBuilder.DropIndex(
                name: "IX_gene_variant_literature_pathogenic_support_category",
                table: "gene_variant_literature");

            migrationBuilder.DropIndex(
                name: "IX_gene_variant_GeneId",
                table: "gene_variant");

            migrationBuilder.DropIndex(
                name: "uniq_start_end_codingchange",
                table: "gene_variant");

            migrationBuilder.DropIndex(
                name: "IX_gene_location_GeneLocationId",
                table: "gene_location");

            migrationBuilder.DropIndex(
                name: "unique_hg_version_gene",
                table: "gene_location");

            migrationBuilder.DropIndex(
                name: "unique_call_type_name",
                table: "call_type");

            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "variant_type");

            migrationBuilder.DropColumn(
                name: "pubMedId",
                table: "literature");

            migrationBuilder.DropColumn(
                name: "added_at",
                table: "gene_variant_literature");

            migrationBuilder.DropColumn(
                name: "app_user_id",
                table: "gene_variant_literature");

            migrationBuilder.DropColumn(
                name: "pathogenic_support_category",
                table: "gene_variant_literature");

            migrationBuilder.DropColumn(
                name: "coding_change",
                table: "gene_variant");

            migrationBuilder.DropColumn(
                name: "end",
                table: "gene_variant");

            migrationBuilder.DropColumn(
                name: "chromosome_id",
                table: "gene_location");

            migrationBuilder.DropColumn(
                name: "GeneLocationId",
                table: "gene_location");

            migrationBuilder.RenameColumn(
                name: "GeneId",
                table: "gene_variant",
                newName: "gene_id");

            migrationBuilder.RenameColumn(
                name: "start",
                table: "gene_variant",
                newName: "call_type_id");

            migrationBuilder.RenameColumn(
                name: "known_function",
                table: "gene",
                newName: "known_gene_function");

            migrationBuilder.RenameColumn(
                name: "note",
                table: "annotation",
                newName: "annotation");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "variant_type",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "active_date",
                table: "symbol",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<int>(
                name: "literature_id",
                table: "gene_variant_literature",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "supports_pathogenicity",
                table: "gene_variant_literature",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "gene_id",
                table: "gene_variant",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "chr",
                table: "gene_location",
                type: "varchar(5)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "chromosome_id",
                table: "gene",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "last_modified_by",
                table: "gene",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified_date",
                table: "gene",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "alternate_gene_name",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gene_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alternate_gene_name", x => x.id);
                    table.ForeignKey(
                        name: "FK_alternate_gene_name_gene_gene_id",
                        column: x => x.gene_id,
                        principalTable: "gene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_gene_variant_call_type_id",
                table: "gene_variant",
                column: "call_type_id");

            migrationBuilder.CreateIndex(
                name: "uniq_gene_zygosity_variant",
                table: "gene_variant",
                columns: new[] { "gene_id", "zygosity_type_id", "variant_type_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gene_chromosome_id",
                table: "gene",
                column: "chromosome_id");

            migrationBuilder.CreateIndex(
                name: "IX_alternate_gene_name_gene_id",
                table: "alternate_gene_name",
                column: "gene_id");

            migrationBuilder.AddForeignKey(
                name: "FK_gene_chromosome_chromosome_id",
                table: "gene",
                column: "chromosome_id",
                principalTable: "chromosome",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gene_variant_call_type_call_type_id",
                table: "gene_variant",
                column: "call_type_id",
                principalTable: "call_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gene_variant_gene_gene_id",
                table: "gene_variant",
                column: "gene_id",
                principalTable: "gene",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gene_variant_literature_literature_literature_id",
                table: "gene_variant_literature",
                column: "literature_id",
                principalTable: "literature",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
