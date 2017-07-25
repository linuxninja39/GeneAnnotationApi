using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GeneAnnotationApi.Entities
{
    public partial class GeneAnnotationDBContext : DbContext
    {
        public virtual DbSet<Accession> Accession { get; set; }
        public virtual DbSet<AlternateGeneName> AlternateGeneName { get; set; }
        public virtual DbSet<Annotation> Annotation { get; set; }
        public virtual DbSet<AnnotationAuthor> AnnotationAuthor { get; set; }
        public virtual DbSet<AnnotationGene> AnnotationGene { get; set; }
        public virtual DbSet<AnnotationGeneVariant> AnnotationGeneVariant { get; set; }
        public virtual DbSet<AnnotationGeneVariantLiterature> AnnotationGeneVariantLiterature { get; set; }
        public virtual DbSet<AnnotationLiterature> AnnotationLiterature { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<AuthorLiterature> AuthorLiterature { get; set; }
        public virtual DbSet<CallType> CallType { get; set; }
        public virtual DbSet<Disorder> Disorder { get; set; }
        public virtual DbSet<Gene> Gene { get; set; }
        public virtual DbSet<GeneLocation> GeneLocation { get; set; }
        public virtual DbSet<GeneName> GeneName { get; set; }
        public virtual DbSet<GeneOriginType> GeneOriginType { get; set; }
        public virtual DbSet<GeneVarLitDisorder> GeneVarLitDisorder { get; set; }
        public virtual DbSet<GeneVariant> GeneVariant { get; set; }
        public virtual DbSet<GeneVariantLiterature> GeneVariantLiterature { get; set; }
        public virtual DbSet<HumanGenomeAssembly> HumanGenomeAssembly { get; set; }
        public virtual DbSet<Literature> Literature { get; set; }
        public virtual DbSet<NameSynonym> NameSynonym { get; set; }
        public virtual DbSet<OriginType> OriginType { get; set; }
        public virtual DbSet<Symbol> Symbol { get; set; }
        public virtual DbSet<Synonym> Synonym { get; set; }
        public virtual DbSet<VariantType> VariantType { get; set; }
        public virtual DbSet<ZygosityType> ZygosityType { get; set; }

        public GeneAnnotationDBContext(DbContextOptions<GeneAnnotationDBContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accession>(entity =>
            {
                entity.HasIndex(e => e.AccessionNumber)
                    .HasName("UQ__accessio__DD2FB278D74E8212")
                    .IsUnique();
            });

            modelBuilder.Entity<Annotation>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<AnnotationAuthor>(entity =>
            {
                entity.HasIndex(e => new { e.AnnotationId, e.AuthorId })
                    .HasName("unique_annotation_author")
                    .IsUnique();
            });

            modelBuilder.Entity<AnnotationGene>(entity =>
            {
                entity.HasIndex(e => new { e.AnnotationId, e.GeneId })
                    .HasName("unique_annotation_gene")
                    .IsUnique();
            });

            modelBuilder.Entity<AnnotationGeneVariant>(entity =>
            {
                entity.HasIndex(e => new { e.AnnotationId, e.GeneVariantId })
                    .HasName("unique_annotation_gene_variant")
                    .IsUnique();
            });

            modelBuilder.Entity<AnnotationGeneVariantLiterature>(entity =>
            {
                entity.HasIndex(e => new { e.AnnotationId, e.GeneVariantLiteratureId })
                    .HasName("unique_annotation_gene_variant_literature")
                    .IsUnique();
            });

            modelBuilder.Entity<AnnotationLiterature>(entity =>
            {
                entity.HasIndex(e => new { e.AnnotationId, e.LiteratureId })
                    .HasName("unique_annotation_literature")
                    .IsUnique();
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__app_user__72E12F1BFFA164A6")
                    .IsUnique();
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__literatu__72E12F1B656D4B26")
                    .IsUnique();
            });

            modelBuilder.Entity<AuthorLiterature>(entity =>
            {
                entity.HasIndex(e => new { e.AuthorId, e.LiteratureId })
                    .HasName("unique_author_literature")
                    .IsUnique();
            });

            modelBuilder.Entity<GeneName>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__gene_nam__72E12F1BF8484542")
                    .IsUnique();
            });

            modelBuilder.Entity<GeneOriginType>(entity =>
            {
                entity.HasIndex(e => new { e.GeneId, e.OriginTypeId })
                    .HasName("unique_gene_origin_type")
                    .IsUnique();
            });

            modelBuilder.Entity<GeneVarLitDisorder>(entity =>
            {
                entity.HasKey(e => new { e.GeneVarLitId, e.DisorderId })
                    .HasName("PK__gene_var__E586E10FA9F56FED");
            });

            modelBuilder.Entity<GeneVariant>(entity =>
            {
                entity.HasIndex(e => new { e.GeneId, e.ZygosityTypeId, e.VariantTypeId })
                    .HasName("uniq_gene_zygosity_variant")
                    .IsUnique();
            });

            modelBuilder.Entity<HumanGenomeAssembly>(entity =>
            {
                entity.HasIndex(e => e.Hg)
                    .HasName("human_genome_assembly_hg_uindex")
                    .IsUnique();
            });

            modelBuilder.Entity<NameSynonym>(entity =>
            {
                entity.HasIndex(e => e.Synonum)
                    .HasName("UQ__name_syn__8E98347ADA10C4C5")
                    .IsUnique();
            });

            modelBuilder.Entity<Symbol>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__symbol__72E12F1B5A73AE92")
                    .IsUnique();
            });

            modelBuilder.Entity<Synonym>(entity =>
            {
                entity.HasIndex(e => e.Synonum)
                    .HasName("UQ__synonym__8E98347A41BDD768")
                    .IsUnique();
            });

            modelBuilder.Entity<ZygosityType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("uniq_name")
                    .IsUnique();
            });
        }
    }
}