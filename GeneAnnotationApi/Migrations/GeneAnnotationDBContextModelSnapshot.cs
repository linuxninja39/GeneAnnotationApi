using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Migrations
{
    [DbContext(typeof(GeneAnnotationDBContext))]
    partial class GeneAnnotationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GeneAnnotationApi.Entities.Accession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("AccessionNumber")
                        .IsRequired()
                        .HasColumnName("accession_number")
                        .HasColumnType("varchar(250)");

                    b.Property<int?>("GeneId")
                        .HasColumnName("gene_id");

                    b.HasKey("Id");

                    b.HasIndex("AccessionNumber")
                        .IsUnique()
                        .HasName("UQ__accessio__DD2FB278D74E8212");

                    b.HasIndex("GeneId");

                    b.ToTable("accession");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AlternateGeneName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("GeneId")
                        .HasColumnName("gene_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("GeneId");

                    b.ToTable("alternate_gene_name");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.Annotation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("AppUserId")
                        .HasColumnName("app_user_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnName("annotation");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("annotation");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AnnotationAuthor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("AnnotationId")
                        .IsRequired()
                        .HasColumnName("annotation_id");

                    b.Property<int?>("AuthorId")
                        .IsRequired()
                        .HasColumnName("author_id");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("AnnotationId", "AuthorId")
                        .IsUnique()
                        .HasName("unique_annotation_author");

                    b.ToTable("annotation_author");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AnnotationGene", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("AnnotationId")
                        .IsRequired()
                        .HasColumnName("annotation_id");

                    b.Property<int?>("GeneId")
                        .IsRequired()
                        .HasColumnName("gene_id");

                    b.HasKey("Id");

                    b.HasIndex("GeneId");

                    b.HasIndex("AnnotationId", "GeneId")
                        .IsUnique()
                        .HasName("unique_annotation_gene");

                    b.ToTable("annotation_gene");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AnnotationGeneVariant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("AnnotationId")
                        .IsRequired()
                        .HasColumnName("annotation_id");

                    b.Property<int?>("GeneVariantId")
                        .IsRequired()
                        .HasColumnName("gene_variant_id");

                    b.HasKey("Id");

                    b.HasIndex("GeneVariantId");

                    b.HasIndex("AnnotationId", "GeneVariantId")
                        .IsUnique()
                        .HasName("unique_annotation_gene_variant");

                    b.ToTable("annotation_gene_variant");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AnnotationGeneVariantLiterature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("AnnotationId")
                        .IsRequired()
                        .HasColumnName("annotation_id");

                    b.Property<int?>("GeneVariantLiteratureId")
                        .IsRequired()
                        .HasColumnName("gene_variant_literature_id");

                    b.HasKey("Id");

                    b.HasIndex("GeneVariantLiteratureId");

                    b.HasIndex("AnnotationId", "GeneVariantLiteratureId")
                        .IsUnique()
                        .HasName("unique_annotation_gene_variant_literature");

                    b.ToTable("annotation_gene_variant_literature");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AnnotationLiterature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("AnnotationId")
                        .IsRequired()
                        .HasColumnName("annotation_id");

                    b.Property<int?>("LiteratureId")
                        .IsRequired()
                        .HasColumnName("literature_id");

                    b.HasKey("Id");

                    b.HasIndex("LiteratureId");

                    b.HasIndex("AnnotationId", "LiteratureId")
                        .IsUnique()
                        .HasName("unique_annotation_literature");

                    b.ToTable("annotation_literature");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UQ__app_user__72E12F1BFFA164A6");

                    b.ToTable("app_user");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UQ__literatu__72E12F1B656D4B26");

                    b.ToTable("author");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AuthorLiterature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("AuthorId")
                        .IsRequired()
                        .HasColumnName("author_id");

                    b.Property<int?>("LiteratureId")
                        .IsRequired()
                        .HasColumnName("literature_id");

                    b.HasKey("Id");

                    b.HasIndex("LiteratureId");

                    b.HasIndex("AuthorId", "LiteratureId")
                        .IsUnique()
                        .HasName("unique_author_literature");

                    b.ToTable("author_literature");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.CallType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("call_type");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.Chromosome", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UQ__chromoso__72E12F1B32E70F94");

                    b.ToTable("chromosome");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.Disorder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("disorder");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.Gene", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("ChromosomeId")
                        .HasColumnName("chromosome_id");

                    b.Property<string>("EnsembleId")
                        .HasColumnName("ensemble_id")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("GeneNameExpansion")
                        .HasColumnName("gene_name_expansion")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("KnownGeneFunction")
                        .HasColumnName("known_gene_function")
                        .HasColumnType("varchar(2000)");

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasColumnName("last_modified_by")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnName("last_modified_date")
                        .HasColumnType("date");

                    b.Property<int?>("OmimId")
                        .HasColumnName("omim_id");

                    b.Property<string>("Refseq")
                        .HasColumnName("refseq")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Ucsc")
                        .HasColumnName("ucsc")
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("ChromosomeId");

                    b.ToTable("gene");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.GeneLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Chr")
                        .IsRequired()
                        .HasColumnName("chr")
                        .HasColumnType("varchar(5)");

                    b.Property<int>("End")
                        .HasColumnName("end");

                    b.Property<int>("GeneId")
                        .HasColumnName("gene_id");

                    b.Property<int>("HgVersion")
                        .HasColumnName("hg_version");

                    b.Property<string>("Locus")
                        .IsRequired()
                        .HasColumnName("locus")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Start")
                        .HasColumnName("start");

                    b.HasKey("Id");

                    b.HasIndex("GeneId");

                    b.ToTable("gene_location");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.GeneName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("ActiveDate")
                        .HasColumnName("active_date")
                        .HasColumnType("datetime");

                    b.Property<int?>("GeneId")
                        .HasColumnName("gene_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("GeneId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UQ__gene_nam__72E12F1BF8484542");

                    b.ToTable("gene_name");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.GeneOriginType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("GeneId")
                        .IsRequired()
                        .HasColumnName("gene_id");

                    b.Property<int?>("OriginTypeId")
                        .IsRequired()
                        .HasColumnName("origin_type_id");

                    b.HasKey("Id");

                    b.HasIndex("OriginTypeId");

                    b.HasIndex("GeneId", "OriginTypeId")
                        .IsUnique()
                        .HasName("unique_gene_origin_type");

                    b.ToTable("gene_origin_type");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.GeneVariant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("CallTypeId")
                        .HasColumnName("call_type_id");

                    b.Property<int>("GeneId")
                        .HasColumnName("gene_id");

                    b.Property<int>("VariantTypeId")
                        .HasColumnName("variant_type_id");

                    b.Property<int>("ZygosityTypeId")
                        .HasColumnName("zygosity_type_id");

                    b.HasKey("Id");

                    b.HasIndex("CallTypeId");

                    b.HasIndex("VariantTypeId");

                    b.HasIndex("ZygosityTypeId");

                    b.HasIndex("GeneId", "ZygosityTypeId", "VariantTypeId")
                        .IsUnique()
                        .HasName("uniq_gene_zygosity_variant");

                    b.ToTable("gene_variant");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.GeneVariantLiterature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("GeneVariantId")
                        .HasColumnName("gene_variant_id");

                    b.Property<int?>("LiteratureId")
                        .HasColumnName("literature_id");

                    b.Property<bool?>("SupportsPathogenicity")
                        .HasColumnName("supports_pathogenicity");

                    b.HasKey("Id");

                    b.HasIndex("GeneVariantId");

                    b.HasIndex("LiteratureId");

                    b.ToTable("gene_variant_literature");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.GeneVarLitDisorder", b =>
                {
                    b.Property<int>("GeneVarLitId")
                        .HasColumnName("gene_var_lit_id");

                    b.Property<int>("DisorderId")
                        .HasColumnName("disorder_id");

                    b.HasKey("GeneVarLitId", "DisorderId")
                        .HasName("PK__gene_var__E586E10FA9F56FED");

                    b.HasIndex("DisorderId");

                    b.ToTable("gene_var_lit_disorder");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.HumanGenomeAssembly", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("Hg")
                        .IsRequired()
                        .HasColumnName("hg");

                    b.HasKey("Id");

                    b.HasIndex("Hg")
                        .IsUnique()
                        .HasName("human_genome_assembly_hg_uindex");

                    b.ToTable("human_genome_assembly");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.Literature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("varchar(2000)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnName("url")
                        .HasColumnType("varchar(2000)");

                    b.HasKey("Id");

                    b.ToTable("literature");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.NameSynonym", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("ActiveDate")
                        .HasColumnName("active_date")
                        .HasColumnType("datetime");

                    b.Property<int?>("GeneId")
                        .HasColumnName("gene_id");

                    b.Property<string>("Synonum")
                        .IsRequired()
                        .HasColumnName("synonum")
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("GeneId");

                    b.HasIndex("Synonum")
                        .IsUnique()
                        .HasName("UQ__name_syn__8E98347ADA10C4C5");

                    b.ToTable("name_synonym");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.OriginType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("origin_type");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.Symbol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime?>("ActiveDate")
                        .HasColumnName("active_date")
                        .HasColumnType("datetime");

                    b.Property<int?>("GeneId")
                        .HasColumnName("gene_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("GeneId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UQ__symbol__72E12F1B5A73AE92");

                    b.ToTable("symbol");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.Synonym", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("ActiveDate")
                        .HasColumnName("active_date")
                        .HasColumnType("datetime");

                    b.Property<int?>("GeneId")
                        .HasColumnName("gene_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("GeneId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UQ__synonym__8E98347A41BDD768");

                    b.ToTable("synonym");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.VariantType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("variant_type");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.ZygosityType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("uniq_name");

                    b.ToTable("zygosity_type");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.Accession", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Gene", "Gene")
                        .WithMany("Accession")
                        .HasForeignKey("GeneId");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AlternateGeneName", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Gene", "Gene")
                        .WithMany("AlternateGeneName")
                        .HasForeignKey("GeneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.Annotation", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.AppUser", "AppUser")
                        .WithMany("Annotation")
                        .HasForeignKey("AppUserId");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AnnotationAuthor", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Annotation", "Annotation")
                        .WithMany("AnnotationAuthor")
                        .HasForeignKey("AnnotationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneAnnotationApi.Entities.Author", "Author")
                        .WithMany("AnnotationAuthor")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AnnotationGene", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Annotation", "Annotation")
                        .WithMany("AnnotationGene")
                        .HasForeignKey("AnnotationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneAnnotationApi.Entities.Gene", "Gene")
                        .WithMany("AnnotationGene")
                        .HasForeignKey("GeneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AnnotationGeneVariant", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Annotation", "Annotation")
                        .WithMany("AnnotationGeneVariant")
                        .HasForeignKey("AnnotationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneAnnotationApi.Entities.GeneVariant", "GeneVariant")
                        .WithMany("AnnotationGeneVariant")
                        .HasForeignKey("GeneVariantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AnnotationGeneVariantLiterature", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Annotation", "Annotation")
                        .WithMany("AnnotationGeneVariantLiterature")
                        .HasForeignKey("AnnotationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneAnnotationApi.Entities.GeneVariantLiterature", "GeneVariantLiterature")
                        .WithMany("AnnotationGeneVariantLiterature")
                        .HasForeignKey("GeneVariantLiteratureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AnnotationLiterature", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Annotation", "Annotation")
                        .WithMany("AnnotationLiterature")
                        .HasForeignKey("AnnotationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneAnnotationApi.Entities.Literature", "Literature")
                        .WithMany("AnnotationLiterature")
                        .HasForeignKey("LiteratureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.AuthorLiterature", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Author", "Author")
                        .WithMany("AuthorLiterature")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneAnnotationApi.Entities.Literature", "Literature")
                        .WithMany("AuthorLiterature")
                        .HasForeignKey("LiteratureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.Gene", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Chromosome", "Chromosome")
                        .WithMany("Gene")
                        .HasForeignKey("ChromosomeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.GeneLocation", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Gene", "Gene")
                        .WithMany("GeneLocation")
                        .HasForeignKey("GeneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.GeneName", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Gene", "Gene")
                        .WithMany("GeneName")
                        .HasForeignKey("GeneId");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.GeneOriginType", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Gene", "Gene")
                        .WithMany("GeneOriginType")
                        .HasForeignKey("GeneId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneAnnotationApi.Entities.OriginType", "OriginType")
                        .WithMany("GeneOriginType")
                        .HasForeignKey("OriginTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.GeneVariant", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.CallType", "CallType")
                        .WithMany("GeneVariant")
                        .HasForeignKey("CallTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneAnnotationApi.Entities.Gene", "Gene")
                        .WithMany("GeneVariant")
                        .HasForeignKey("GeneId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneAnnotationApi.Entities.VariantType", "VariantType")
                        .WithMany("GeneVariant")
                        .HasForeignKey("VariantTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneAnnotationApi.Entities.ZygosityType", "ZygosityType")
                        .WithMany("GeneVariant")
                        .HasForeignKey("ZygosityTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.GeneVariantLiterature", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.GeneVariant", "GeneVariant")
                        .WithMany("GeneVariantLiterature")
                        .HasForeignKey("GeneVariantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneAnnotationApi.Entities.Literature", "Literature")
                        .WithMany("GeneVariantLiterature")
                        .HasForeignKey("LiteratureId");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.GeneVarLitDisorder", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Disorder", "Disorder")
                        .WithMany("GeneVarLitDisorder")
                        .HasForeignKey("DisorderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GeneAnnotationApi.Entities.GeneVariantLiterature", "GeneVarLit")
                        .WithMany("GeneVarLitDisorder")
                        .HasForeignKey("GeneVarLitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.NameSynonym", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Gene", "Gene")
                        .WithMany("NameSynonym")
                        .HasForeignKey("GeneId");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.Symbol", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Gene", "Gene")
                        .WithMany("Symbol")
                        .HasForeignKey("GeneId");
                });

            modelBuilder.Entity("GeneAnnotationApi.Entities.Synonym", b =>
                {
                    b.HasOne("GeneAnnotationApi.Entities.Gene", "Gene")
                        .WithMany("Synonym")
                        .HasForeignKey("GeneId");
                });
        }
    }
}
