using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GeneAnnotationApi.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "app_user",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "call_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_call_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chromosome",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chromosome", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "disorder",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disorder", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "human_genome_assembly",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    hg = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_human_genome_assembly", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "literature",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(2000)", nullable: false),
                    url = table.Column<string>(type: "varchar(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_literature", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "origin_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_origin_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "variant_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_variant_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "zygosity_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zygosity_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "annotation",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    app_user_id = table.Column<int>(nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    annotation = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_annotation", x => x.id);
                    table.ForeignKey(
                        name: "FK_annotation_app_user_app_user_id",
                        column: x => x.app_user_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gene",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    chromosome_id = table.Column<int>(nullable: false),
                    ensemble_id = table.Column<string>(type: "varchar(250)", nullable: true),
                    gene_name_expansion = table.Column<string>(type: "varchar(1000)", nullable: true),
                    known_gene_function = table.Column<string>(type: "varchar(2000)", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(255)", nullable: false),
                    last_modified_date = table.Column<DateTime>(type: "date", nullable: false),
                    omim_id = table.Column<int>(nullable: true),
                    refseq = table.Column<string>(type: "varchar(250)", nullable: true),
                    ucsc = table.Column<string>(type: "varchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gene", x => x.id);
                    table.ForeignKey(
                        name: "FK_gene_chromosome_chromosome_id",
                        column: x => x.chromosome_id,
                        principalTable: "chromosome",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "author_literature",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    author_id = table.Column<int>(nullable: false),
                    literature_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author_literature", x => x.id);
                    table.ForeignKey(
                        name: "FK_author_literature_author_author_id",
                        column: x => x.author_id,
                        principalTable: "author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_author_literature_literature_literature_id",
                        column: x => x.literature_id,
                        principalTable: "literature",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "annotation_author",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    annotation_id = table.Column<int>(nullable: false),
                    author_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_annotation_author", x => x.id);
                    table.ForeignKey(
                        name: "FK_annotation_author_annotation_annotation_id",
                        column: x => x.annotation_id,
                        principalTable: "annotation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_annotation_author_author_author_id",
                        column: x => x.author_id,
                        principalTable: "author",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "annotation_literature",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    annotation_id = table.Column<int>(nullable: false),
                    literature_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_annotation_literature", x => x.id);
                    table.ForeignKey(
                        name: "FK_annotation_literature_annotation_annotation_id",
                        column: x => x.annotation_id,
                        principalTable: "annotation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_annotation_literature_literature_literature_id",
                        column: x => x.literature_id,
                        principalTable: "literature",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "accession",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    accession_number = table.Column<string>(type: "varchar(250)", nullable: false),
                    gene_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accession", x => x.id);
                    table.ForeignKey(
                        name: "FK_accession_gene_gene_id",
                        column: x => x.gene_id,
                        principalTable: "gene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "annotation_gene",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    annotation_id = table.Column<int>(nullable: false),
                    gene_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_annotation_gene", x => x.id);
                    table.ForeignKey(
                        name: "FK_annotation_gene_annotation_annotation_id",
                        column: x => x.annotation_id,
                        principalTable: "annotation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_annotation_gene_gene_gene_id",
                        column: x => x.gene_id,
                        principalTable: "gene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gene_location",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    chr = table.Column<string>(type: "varchar(5)", nullable: false),
                    end = table.Column<int>(nullable: false),
                    gene_id = table.Column<int>(nullable: false),
                    hg_version = table.Column<int>(nullable: false),
                    locus = table.Column<string>(type: "varchar(20)", nullable: false),
                    start = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gene_location", x => x.id);
                    table.ForeignKey(
                        name: "FK_gene_location_gene_gene_id",
                        column: x => x.gene_id,
                        principalTable: "gene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gene_name",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    active_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    gene_id = table.Column<int>(nullable: true),
                    name = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gene_name", x => x.id);
                    table.ForeignKey(
                        name: "FK_gene_name_gene_gene_id",
                        column: x => x.gene_id,
                        principalTable: "gene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "gene_origin_type",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gene_id = table.Column<int>(nullable: false),
                    origin_type_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gene_origin_type", x => x.id);
                    table.ForeignKey(
                        name: "FK_gene_origin_type_gene_gene_id",
                        column: x => x.gene_id,
                        principalTable: "gene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gene_origin_type_origin_type_origin_type_id",
                        column: x => x.origin_type_id,
                        principalTable: "origin_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gene_variant",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    call_type_id = table.Column<int>(nullable: false),
                    gene_id = table.Column<int>(nullable: false),
                    variant_type_id = table.Column<int>(nullable: false),
                    zygosity_type_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gene_variant", x => x.id);
                    table.ForeignKey(
                        name: "FK_gene_variant_call_type_call_type_id",
                        column: x => x.call_type_id,
                        principalTable: "call_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gene_variant_gene_gene_id",
                        column: x => x.gene_id,
                        principalTable: "gene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gene_variant_variant_type_variant_type_id",
                        column: x => x.variant_type_id,
                        principalTable: "variant_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gene_variant_zygosity_type_zygosity_type_id",
                        column: x => x.zygosity_type_id,
                        principalTable: "zygosity_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "name_synonym",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    active_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    gene_id = table.Column<int>(nullable: true),
                    synonum = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_name_synonym", x => x.id);
                    table.ForeignKey(
                        name: "FK_name_synonym_gene_gene_id",
                        column: x => x.gene_id,
                        principalTable: "gene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "symbol",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    active_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    gene_id = table.Column<int>(nullable: true),
                    name = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_symbol", x => x.id);
                    table.ForeignKey(
                        name: "FK_symbol_gene_gene_id",
                        column: x => x.gene_id,
                        principalTable: "gene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "synonym",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    active_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    gene_id = table.Column<int>(nullable: true),
                    name = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_synonym", x => x.id);
                    table.ForeignKey(
                        name: "FK_synonym_gene_gene_id",
                        column: x => x.gene_id,
                        principalTable: "gene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "annotation_gene_variant",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    annotation_id = table.Column<int>(nullable: false),
                    gene_variant_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_annotation_gene_variant", x => x.id);
                    table.ForeignKey(
                        name: "FK_annotation_gene_variant_annotation_annotation_id",
                        column: x => x.annotation_id,
                        principalTable: "annotation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_annotation_gene_variant_gene_variant_gene_variant_id",
                        column: x => x.gene_variant_id,
                        principalTable: "gene_variant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gene_variant_literature",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gene_variant_id = table.Column<int>(nullable: false),
                    literature_id = table.Column<int>(nullable: true),
                    supports_pathogenicity = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gene_variant_literature", x => x.id);
                    table.ForeignKey(
                        name: "FK_gene_variant_literature_gene_variant_gene_variant_id",
                        column: x => x.gene_variant_id,
                        principalTable: "gene_variant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gene_variant_literature_literature_literature_id",
                        column: x => x.literature_id,
                        principalTable: "literature",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "annotation_gene_variant_literature",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    annotation_id = table.Column<int>(nullable: false),
                    gene_variant_literature_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_annotation_gene_variant_literature", x => x.id);
                    table.ForeignKey(
                        name: "FK_annotation_gene_variant_literature_annotation_annotation_id",
                        column: x => x.annotation_id,
                        principalTable: "annotation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_annotation_gene_variant_literature_gene_variant_literature_gene_variant_literature_id",
                        column: x => x.gene_variant_literature_id,
                        principalTable: "gene_variant_literature",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gene_var_lit_disorder",
                columns: table => new
                {
                    gene_var_lit_id = table.Column<int>(nullable: false),
                    disorder_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__gene_var__E586E10FA9F56FED", x => new { x.gene_var_lit_id, x.disorder_id });
                    table.ForeignKey(
                        name: "FK_gene_var_lit_disorder_disorder_disorder_id",
                        column: x => x.disorder_id,
                        principalTable: "disorder",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gene_var_lit_disorder_gene_variant_literature_gene_var_lit_id",
                        column: x => x.gene_var_lit_id,
                        principalTable: "gene_variant_literature",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__accessio__DD2FB278D74E8212",
                table: "accession",
                column: "accession_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_accession_gene_id",
                table: "accession",
                column: "gene_id");

            migrationBuilder.CreateIndex(
                name: "IX_alternate_gene_name_gene_id",
                table: "alternate_gene_name",
                column: "gene_id");

            migrationBuilder.CreateIndex(
                name: "IX_annotation_app_user_id",
                table: "annotation",
                column: "app_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_annotation_author_author_id",
                table: "annotation_author",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "unique_annotation_author",
                table: "annotation_author",
                columns: new[] { "annotation_id", "author_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_annotation_gene_gene_id",
                table: "annotation_gene",
                column: "gene_id");

            migrationBuilder.CreateIndex(
                name: "unique_annotation_gene",
                table: "annotation_gene",
                columns: new[] { "annotation_id", "gene_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_annotation_gene_variant_gene_variant_id",
                table: "annotation_gene_variant",
                column: "gene_variant_id");

            migrationBuilder.CreateIndex(
                name: "unique_annotation_gene_variant",
                table: "annotation_gene_variant",
                columns: new[] { "annotation_id", "gene_variant_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_annotation_gene_variant_literature_gene_variant_literature_id",
                table: "annotation_gene_variant_literature",
                column: "gene_variant_literature_id");

            migrationBuilder.CreateIndex(
                name: "unique_annotation_gene_variant_literature",
                table: "annotation_gene_variant_literature",
                columns: new[] { "annotation_id", "gene_variant_literature_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_annotation_literature_literature_id",
                table: "annotation_literature",
                column: "literature_id");

            migrationBuilder.CreateIndex(
                name: "unique_annotation_literature",
                table: "annotation_literature",
                columns: new[] { "annotation_id", "literature_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__app_user__72E12F1BFFA164A6",
                table: "app_user",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__literatu__72E12F1B656D4B26",
                table: "author",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_author_literature_literature_id",
                table: "author_literature",
                column: "literature_id");

            migrationBuilder.CreateIndex(
                name: "unique_author_literature",
                table: "author_literature",
                columns: new[] { "author_id", "literature_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__chromoso__72E12F1B32E70F94",
                table: "chromosome",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gene_chromosome_id",
                table: "gene",
                column: "chromosome_id");

            migrationBuilder.CreateIndex(
                name: "IX_gene_location_gene_id",
                table: "gene_location",
                column: "gene_id");

            migrationBuilder.CreateIndex(
                name: "IX_gene_name_gene_id",
                table: "gene_name",
                column: "gene_id");

            migrationBuilder.CreateIndex(
                name: "UQ__gene_nam__72E12F1BF8484542",
                table: "gene_name",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gene_origin_type_origin_type_id",
                table: "gene_origin_type",
                column: "origin_type_id");

            migrationBuilder.CreateIndex(
                name: "unique_gene_origin_type",
                table: "gene_origin_type",
                columns: new[] { "gene_id", "origin_type_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gene_variant_call_type_id",
                table: "gene_variant",
                column: "call_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_gene_variant_variant_type_id",
                table: "gene_variant",
                column: "variant_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_gene_variant_zygosity_type_id",
                table: "gene_variant",
                column: "zygosity_type_id");

            migrationBuilder.CreateIndex(
                name: "uniq_gene_zygosity_variant",
                table: "gene_variant",
                columns: new[] { "gene_id", "zygosity_type_id", "variant_type_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gene_variant_literature_gene_variant_id",
                table: "gene_variant_literature",
                column: "gene_variant_id");

            migrationBuilder.CreateIndex(
                name: "IX_gene_variant_literature_literature_id",
                table: "gene_variant_literature",
                column: "literature_id");

            migrationBuilder.CreateIndex(
                name: "IX_gene_var_lit_disorder_disorder_id",
                table: "gene_var_lit_disorder",
                column: "disorder_id");

            migrationBuilder.CreateIndex(
                name: "human_genome_assembly_hg_uindex",
                table: "human_genome_assembly",
                column: "hg",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_name_synonym_gene_id",
                table: "name_synonym",
                column: "gene_id");

            migrationBuilder.CreateIndex(
                name: "UQ__name_syn__8E98347ADA10C4C5",
                table: "name_synonym",
                column: "synonum",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_symbol_gene_id",
                table: "symbol",
                column: "gene_id");

            migrationBuilder.CreateIndex(
                name: "UQ__symbol__72E12F1B5A73AE92",
                table: "symbol",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_synonym_gene_id",
                table: "synonym",
                column: "gene_id");

            migrationBuilder.CreateIndex(
                name: "UQ__synonym__8E98347A41BDD768",
                table: "synonym",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uniq_name",
                table: "zygosity_type",
                column: "name",
                unique: true);
            
            InitialCreationData.Up(migrationBuilder);
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accession");

            migrationBuilder.DropTable(
                name: "alternate_gene_name");

            migrationBuilder.DropTable(
                name: "annotation_author");

            migrationBuilder.DropTable(
                name: "annotation_gene");

            migrationBuilder.DropTable(
                name: "annotation_gene_variant");

            migrationBuilder.DropTable(
                name: "annotation_gene_variant_literature");

            migrationBuilder.DropTable(
                name: "annotation_literature");

            migrationBuilder.DropTable(
                name: "author_literature");

            migrationBuilder.DropTable(
                name: "gene_location");

            migrationBuilder.DropTable(
                name: "gene_name");

            migrationBuilder.DropTable(
                name: "gene_origin_type");

            migrationBuilder.DropTable(
                name: "gene_var_lit_disorder");

            migrationBuilder.DropTable(
                name: "human_genome_assembly");

            migrationBuilder.DropTable(
                name: "name_synonym");

            migrationBuilder.DropTable(
                name: "symbol");

            migrationBuilder.DropTable(
                name: "synonym");

            migrationBuilder.DropTable(
                name: "annotation");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "origin_type");

            migrationBuilder.DropTable(
                name: "disorder");

            migrationBuilder.DropTable(
                name: "gene_variant_literature");

            migrationBuilder.DropTable(
                name: "app_user");

            migrationBuilder.DropTable(
                name: "gene_variant");

            migrationBuilder.DropTable(
                name: "literature");

            migrationBuilder.DropTable(
                name: "call_type");

            migrationBuilder.DropTable(
                name: "gene");

            migrationBuilder.DropTable(
                name: "variant_type");

            migrationBuilder.DropTable(
                name: "zygosity_type");

            migrationBuilder.DropTable(
                name: "chromosome");
        }
    }
}
