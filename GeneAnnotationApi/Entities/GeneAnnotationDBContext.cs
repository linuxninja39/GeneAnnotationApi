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
        public virtual DbSet<Literature> Literature { get; set; }
        public virtual DbSet<NameSynonym> NameSynonym { get; set; }
        public virtual DbSet<OriginType> OriginType { get; set; }
        public virtual DbSet<Symbol> Symbol { get; set; }
        public virtual DbSet<Synonym> Synonym { get; set; }
        public virtual DbSet<VariantType> VariantType { get; set; }
        public virtual DbSet<ZygosityType> ZygosityType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=lineagen-svr02;Database=GeneAnnotationDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accession>(entity =>
            {
                entity.ToTable("accession");

                entity.HasIndex(e => e.AccessionNumber)
                    .HasName("UQ__accessio__DD2FB278D74E8212")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccessionNumber)
                    .IsRequired()
                    .HasColumnName("accession_number")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.GeneId).HasColumnName("gene_id");

                entity.HasOne(d => d.Gene)
                    .WithMany(p => p.Accession)
                    .HasForeignKey(d => d.GeneId)
                    .HasConstraintName("FK__accession__gene___72C60C4A");
            });

            modelBuilder.Entity<AlternateGeneName>(entity =>
            {
                entity.ToTable("alternate_gene_name");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GeneId).HasColumnName("gene_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");

                entity.HasOne(d => d.Gene)
                    .WithMany(p => p.AlternateGeneName)
                    .HasForeignKey(d => d.GeneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_alternate_gene_name_gene_1");
            });

            modelBuilder.Entity<Annotation>(entity =>
            {
                entity.ToTable("annotation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Annotation1)
                    .IsRequired()
                    .HasColumnName("annotation");

                entity.Property(e => e.AppUserId).HasColumnName("app_user_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.Annotation)
                    .HasForeignKey(d => d.AppUserId)
                    .HasConstraintName("FK__annotatio__app_u__398D8EEE");
            });

            modelBuilder.Entity<AnnotationAuthor>(entity =>
            {
                entity.ToTable("annotation_author");

                entity.HasIndex(e => new { e.AnnotationId, e.AuthorId })
                    .HasName("unique_annotation_author")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnnotationId)
                    .IsRequired()
                    .HasColumnName("annotation_id");

                entity.Property(e => e.AuthorId)
                    .IsRequired()
                    .HasColumnName("author_id");

                entity.HasOne(d => d.Annotation)
                    .WithMany(p => p.AnnotationAuthor)
                    .HasForeignKey(d => d.AnnotationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__annotatio__annot__59063A47");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AnnotationAuthor)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__annotatio__autho__59FA5E80");
            });

            modelBuilder.Entity<AnnotationGene>(entity =>
            {
                entity.ToTable("annotation_gene");

                entity.HasIndex(e => new { e.AnnotationId, e.GeneId })
                    .HasName("unique_annotation_gene")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnnotationId)
                    .IsRequired()
                    .HasColumnName("annotation_id");

                entity.Property(e => e.GeneId)
                    .IsRequired()
                    .HasColumnName("gene_id");

                entity.HasOne(d => d.Annotation)
                    .WithMany(p => p.AnnotationGene)
                    .HasForeignKey(d => d.AnnotationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__annotatio__annot__4AB81AF0");

                entity.HasOne(d => d.Gene)
                    .WithMany(p => p.AnnotationGene)
                    .HasForeignKey(d => d.GeneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__annotatio__gene___4BAC3F29");
            });

            modelBuilder.Entity<AnnotationGeneVariant>(entity =>
            {
                entity.ToTable("annotation_gene_variant");

                entity.HasIndex(e => new { e.AnnotationId, e.GeneVariantId })
                    .HasName("unique_annotation_gene_variant")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnnotationId)
                    .IsRequired()
                    .HasColumnName("annotation_id");

                entity.Property(e => e.GeneVariantId)
                    .IsRequired()
                    .HasColumnName("gene_variant_id");

                entity.HasOne(d => d.Annotation)
                    .WithMany(p => p.AnnotationGeneVariant)
                    .HasForeignKey(d => d.AnnotationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__annotatio__annot__4F7CD00D");

                entity.HasOne(d => d.GeneVariant)
                    .WithMany(p => p.AnnotationGeneVariant)
                    .HasForeignKey(d => d.GeneVariantId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__annotatio__gene___5070F446");
            });

            modelBuilder.Entity<AnnotationGeneVariantLiterature>(entity =>
            {
                entity.ToTable("annotation_gene_variant_literature");

                entity.HasIndex(e => new { e.AnnotationId, e.GeneVariantLiteratureId })
                    .HasName("unique_annotation_gene_variant_literature")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnnotationId)
                    .IsRequired()
                    .HasColumnName("annotation_id");

                entity.Property(e => e.GeneVariantLiteratureId)
                    .IsRequired()
                    .HasColumnName("gene_variant_literature_id");

                entity.HasOne(d => d.Annotation)
                    .WithMany(p => p.AnnotationGeneVariantLiterature)
                    .HasForeignKey(d => d.AnnotationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__annotatio__annot__5441852A");

                entity.HasOne(d => d.GeneVariantLiterature)
                    .WithMany(p => p.AnnotationGeneVariantLiterature)
                    .HasForeignKey(d => d.GeneVariantLiteratureId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__annotatio__gene___5535A963");
            });

            modelBuilder.Entity<AnnotationLiterature>(entity =>
            {
                entity.ToTable("annotation_literature");

                entity.HasIndex(e => new { e.AnnotationId, e.LiteratureId })
                    .HasName("unique_annotation_literature")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnnotationId)
                    .IsRequired()
                    .HasColumnName("annotation_id");

                entity.Property(e => e.LiteratureId)
                    .IsRequired()
                    .HasColumnName("literature_id");

                entity.HasOne(d => d.Annotation)
                    .WithMany(p => p.AnnotationLiterature)
                    .HasForeignKey(d => d.AnnotationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__annotatio__annot__45F365D3");

                entity.HasOne(d => d.Literature)
                    .WithMany(p => p.AnnotationLiterature)
                    .HasForeignKey(d => d.LiteratureId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__annotatio__liter__46E78A0C");
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("app_user");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__app_user__72E12F1BFFA164A6")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__literatu__72E12F1B656D4B26")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<AuthorLiterature>(entity =>
            {
                entity.ToTable("author_literature");

                entity.HasIndex(e => new { e.AuthorId, e.LiteratureId })
                    .HasName("unique_author_literature")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorId)
                    .IsRequired()
                    .HasColumnName("author_id");

                entity.Property(e => e.LiteratureId)
                    .IsRequired()
                    .HasColumnName("literature_id");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorLiterature)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__author_li__autho__403A8C7D");

                entity.HasOne(d => d.Literature)
                    .WithMany(p => p.AuthorLiterature)
                    .HasForeignKey(d => d.LiteratureId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__author_li__liter__412EB0B6");
            });

            modelBuilder.Entity<CallType>(entity =>
            {
                entity.ToTable("call_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Disorder>(entity =>
            {
                entity.ToTable("disorder");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Gene>(entity =>
            {
                entity.ToTable("gene");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EnsembleId)
                    .HasColumnName("ensemble_id")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.GeneNameExpansion)
                    .HasColumnName("gene_name_expansion")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.KnownGeneFunction)
                    .HasColumnName("known_gene_function")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.LastModifiedBy)
                    .IsRequired()
                    .HasColumnName("last_modified_by")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnName("last_modified_date")
                    .HasColumnType("date");

                entity.Property(e => e.OmimId).HasColumnName("omim_id");

                entity.Property(e => e.Refseq)
                    .HasColumnName("refseq")
                    .HasColumnType("varchar(250)");

                entity.Property(e => e.Ucsc)
                    .HasColumnName("ucsc")
                    .HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<GeneLocation>(entity =>
            {
                entity.ToTable("gene_location");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Chr)
                    .IsRequired()
                    .HasColumnName("chr")
                    .HasColumnType("varchar(5)");

                entity.Property(e => e.End).HasColumnName("end");

                entity.Property(e => e.GeneId).HasColumnName("gene_id");

                entity.Property(e => e.HgVersion).HasColumnName("hg_version");

                entity.Property(e => e.Locus)
                    .IsRequired()
                    .HasColumnName("locus")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Start).HasColumnName("start");

                entity.HasOne(d => d.Gene)
                    .WithMany(p => p.GeneLocation)
                    .HasForeignKey(d => d.GeneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_gene_location_gene_1");
            });

            modelBuilder.Entity<GeneName>(entity =>
            {
                entity.ToTable("gene_name");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__gene_nam__72E12F1BF8484542")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActiveDate)
                    .HasColumnName("active_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.GeneId).HasColumnName("gene_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(250)");

                entity.HasOne(d => d.Gene)
                    .WithMany(p => p.GeneName)
                    .HasForeignKey(d => d.GeneId)
                    .HasConstraintName("FK__gene_name__gene___66603565");
            });

            modelBuilder.Entity<GeneOriginType>(entity =>
            {
                entity.ToTable("gene_origin_type");

                entity.HasIndex(e => new { e.GeneId, e.OriginTypeId })
                    .HasName("unique_gene_origin_type")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GeneId)
                    .IsRequired()
                    .HasColumnName("gene_id");

                entity.Property(e => e.OriginTypeId)
                    .IsRequired()
                    .HasColumnName("origin_type_id");

                entity.HasOne(d => d.Gene)
                    .WithMany(p => p.GeneOriginType)
                    .HasForeignKey(d => d.GeneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__gene_orig__gene___5DCAEF64");

                entity.HasOne(d => d.OriginType)
                    .WithMany(p => p.GeneOriginType)
                    .HasForeignKey(d => d.OriginTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__gene_orig__origi__5EBF139D");
            });

            modelBuilder.Entity<GeneVarLitDisorder>(entity =>
            {
                entity.HasKey(e => new { e.GeneVarLitId, e.DisorderId })
                    .HasName("PK__gene_var__E586E10FA9F56FED");

                entity.ToTable("gene_var_lit_disorder");

                entity.Property(e => e.GeneVarLitId).HasColumnName("gene_var_lit_id");

                entity.Property(e => e.DisorderId).HasColumnName("disorder_id");

                entity.HasOne(d => d.Disorder)
                    .WithMany(p => p.GeneVarLitDisorder)
                    .HasForeignKey(d => d.DisorderId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_gene_var_lit_disorder_disorder_1");

                entity.HasOne(d => d.GeneVarLit)
                    .WithMany(p => p.GeneVarLitDisorder)
                    .HasForeignKey(d => d.GeneVarLitId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_gene_var_lit_disorder_gene_variant_literature_1");
            });

            modelBuilder.Entity<GeneVariant>(entity =>
            {
                entity.ToTable("gene_variant");

                entity.HasIndex(e => new { e.GeneId, e.ZygosityTypeId, e.VariantTypeId })
                    .HasName("uniq_gene_zygosity_variant")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CallTypeId).HasColumnName("call_type_id");

                entity.Property(e => e.GeneId).HasColumnName("gene_id");

                entity.Property(e => e.VariantTypeId).HasColumnName("variant_type_id");

                entity.Property(e => e.ZygosityTypeId).HasColumnName("zygosity_type_id");

                entity.HasOne(d => d.CallType)
                    .WithMany(p => p.GeneVariant)
                    .HasForeignKey(d => d.CallTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_gene_variant_call_type_1");

                entity.HasOne(d => d.Gene)
                    .WithMany(p => p.GeneVariant)
                    .HasForeignKey(d => d.GeneId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_gene_variant_gene_1");

                entity.HasOne(d => d.VariantType)
                    .WithMany(p => p.GeneVariant)
                    .HasForeignKey(d => d.VariantTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_gene_variant_variant_type_1");

                entity.HasOne(d => d.ZygosityType)
                    .WithMany(p => p.GeneVariant)
                    .HasForeignKey(d => d.ZygosityTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_gene_variant_zygosity_type_1");
            });

            modelBuilder.Entity<GeneVariantLiterature>(entity =>
            {
                entity.ToTable("gene_variant_literature");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GeneVariantId).HasColumnName("gene_variant_id");

                entity.Property(e => e.LiteratureId).HasColumnName("literature_id");

                entity.Property(e => e.SupportsPathogenicity).HasColumnName("supports_pathogenicity");

                entity.HasOne(d => d.GeneVariant)
                    .WithMany(p => p.GeneVariantLiterature)
                    .HasForeignKey(d => d.GeneVariantId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_gene_variant_literature_gene_variant_1");

                entity.HasOne(d => d.Literature)
                    .WithMany(p => p.GeneVariantLiterature)
                    .HasForeignKey(d => d.LiteratureId)
                    .HasConstraintName("fk_gene_variant_literature_literature_1");
            });

            modelBuilder.Entity<Literature>(entity =>
            {
                entity.ToTable("literature");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(2000)");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasColumnType("varchar(2000)");
            });

            modelBuilder.Entity<NameSynonym>(entity =>
            {
                entity.ToTable("name_synonym");

                entity.HasIndex(e => e.Synonum)
                    .HasName("UQ__name_syn__8E98347ADA10C4C5")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActiveDate)
                    .HasColumnName("active_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.GeneId).HasColumnName("gene_id");

                entity.Property(e => e.Synonum)
                    .IsRequired()
                    .HasColumnName("synonum")
                    .HasColumnType("varchar(250)");

                entity.HasOne(d => d.Gene)
                    .WithMany(p => p.NameSynonym)
                    .HasForeignKey(d => d.GeneId)
                    .HasConstraintName("FK__name_syno__gene___6EF57B66");
            });

            modelBuilder.Entity<OriginType>(entity =>
            {
                entity.ToTable("origin_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Symbol>(entity =>
            {
                entity.ToTable("symbol");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__symbol__72E12F1B5A73AE92")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActiveDate)
                    .HasColumnName("active_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.GeneId).HasColumnName("gene_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(10)");

                entity.HasOne(d => d.Gene)
                    .WithMany(p => p.Symbol)
                    .HasForeignKey(d => d.GeneId)
                    .HasConstraintName("FK__symbol__gene_id__628FA481");
            });

            modelBuilder.Entity<Synonym>(entity =>
            {
                entity.ToTable("synonym");

                entity.HasIndex(e => e.Synonum)
                    .HasName("UQ__synonym__8E98347A41BDD768")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActiveDate)
                    .HasColumnName("active_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.GeneId).HasColumnName("gene_id");

                entity.Property(e => e.Synonum)
                    .IsRequired()
                    .HasColumnName("synonum")
                    .HasColumnType("varchar(250)");

                entity.HasOne(d => d.Gene)
                    .WithMany(p => p.Synonym)
                    .HasForeignKey(d => d.GeneId)
                    .HasConstraintName("FK__synonym__gene_id__6A30C649");
            });

            modelBuilder.Entity<VariantType>(entity =>
            {
                entity.ToTable("variant_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<ZygosityType>(entity =>
            {
                entity.ToTable("zygosity_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");
            });
        }
    }
}