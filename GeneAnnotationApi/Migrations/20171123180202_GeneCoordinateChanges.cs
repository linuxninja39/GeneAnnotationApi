using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GeneAnnotationApi.Migrations
{
    public partial class GeneCoordinateChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gene_location_chromosome_GeneLocationId",
                table: "gene_location");

            migrationBuilder.DropIndex(
                name: "IX_gene_location_GeneLocationId",
                table: "gene_location");

            migrationBuilder.DropColumn(
                name: "GeneLocationId",
                table: "gene_location");

            migrationBuilder.CreateTable(
                name: "foreign_entity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foreign_entity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gene_coordinate",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    end = table.Column<int>(nullable: false),
                    gene_location_id = table.Column<int>(nullable: false),
                    start = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gene_coordinate", x => x.id);
                    table.ForeignKey(
                        name: "FK_gene_coordinate_gene_location_gene_location_id",
                        column: x => x.gene_location_id,
                        principalTable: "gene_location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "foreign_indentity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    foreign_entity_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foreign_indentity", x => x.id);
                    table.ForeignKey(
                        name: "FK_foreign_indentity_foreign_entity_foreign_entity_id",
                        column: x => x.foreign_entity_id,
                        principalTable: "foreign_entity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "foreign_identity_gene_coordinate",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    foreign_identity_id = table.Column<int>(nullable: false),
                    gene_coordinate_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foreign_identity_gene_coordinate", x => x.id);
                    table.ForeignKey(
                        name: "FK_foreign_identity_gene_coordinate_foreign_indentity_foreign_identity_id",
                        column: x => x.foreign_identity_id,
                        principalTable: "foreign_indentity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_foreign_identity_gene_coordinate_gene_coordinate_gene_coordinate_id",
                        column: x => x.gene_coordinate_id,
                        principalTable: "gene_coordinate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_gene_location_chromosome_id",
                table: "gene_location",
                column: "chromosome_id");

            migrationBuilder.CreateIndex(
                name: "IX_foreign_indentity_foreign_entity_id",
                table: "foreign_indentity",
                column: "foreign_entity_id");

            migrationBuilder.CreateIndex(
                name: "IX_foreign_identity_gene_coordinate_foreign_identity_id",
                table: "foreign_identity_gene_coordinate",
                column: "foreign_identity_id");

            migrationBuilder.CreateIndex(
                name: "IX_foreign_identity_gene_coordinate_gene_coordinate_id",
                table: "foreign_identity_gene_coordinate",
                column: "gene_coordinate_id");

            migrationBuilder.CreateIndex(
                name: "IX_gene_coordinate_gene_location_id",
                table: "gene_coordinate",
                column: "gene_location_id");

            migrationBuilder.AddForeignKey(
                name: "FK_gene_location_chromosome_chromosome_id",
                table: "gene_location",
                column: "chromosome_id",
                principalTable: "chromosome",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gene_location_chromosome_chromosome_id",
                table: "gene_location");

            migrationBuilder.DropTable(
                name: "foreign_identity_gene_coordinate");

            migrationBuilder.DropTable(
                name: "foreign_indentity");

            migrationBuilder.DropTable(
                name: "gene_coordinate");

            migrationBuilder.DropTable(
                name: "foreign_entity");

            migrationBuilder.DropIndex(
                name: "IX_gene_location_chromosome_id",
                table: "gene_location");

            migrationBuilder.AddColumn<int>(
                name: "GeneLocationId",
                table: "gene_location",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_gene_location_GeneLocationId",
                table: "gene_location",
                column: "GeneLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_gene_location_chromosome_GeneLocationId",
                table: "gene_location",
                column: "GeneLocationId",
                principalTable: "chromosome",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
