using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("gene")]
    public partial class Gene
    {
        public Gene()
        {
            Accession = new HashSet<Accession>();
            AlternateGeneName = new HashSet<AlternateGeneName>();
            AnnotationGene = new HashSet<AnnotationGene>();
            GeneLocation = new HashSet<GeneLocation>();
            GeneName = new HashSet<GeneName>();
            GeneOriginType = new HashSet<GeneOriginType>();
            GeneVariant = new HashSet<GeneVariant>();
            NameSynonym = new HashSet<NameSynonym>();
            Symbol = new HashSet<Symbol>();
            Synonym = new HashSet<Synonym>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("gene_name_expansion", TypeName = "varchar(1000)")]
        public string GeneNameExpansion { get; set; }
        [Column("known_gene_function", TypeName = "varchar(2000)")]
        public string KnownGeneFunction { get; set; }
        [Required]
        [Column("last_modified_by", TypeName = "varchar(255)")]
        public string LastModifiedBy { get; set; }
        [Column("last_modified_date", TypeName = "date")]
        public DateTime LastModifiedDate { get; set; }
        [Column("omim_id")]
        public int? OmimId { get; set; }
        [Column("refseq", TypeName = "varchar(250)")]
        public string Refseq { get; set; }
        [Column("ensemble_id", TypeName = "varchar(250)")]
        public string EnsembleId { get; set; }
        [Column("ucsc", TypeName = "varchar(250)")]
        public string Ucsc { get; set; }
        [Column("human_genome_assembly_id")]
        public int? HumanGenomeAssemblyId { get; set; }
        [Column("chromosome_id")]
        public int ChromosomeId { get; set; }

        [InverseProperty("Gene")]
        public virtual ICollection<Accession> Accession { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<AlternateGeneName> AlternateGeneName { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<AnnotationGene> AnnotationGene { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<GeneLocation> GeneLocation { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<GeneName> GeneName { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<GeneOriginType> GeneOriginType { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<GeneVariant> GeneVariant { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<NameSynonym> NameSynonym { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<Symbol> Symbol { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<Synonym> Synonym { get; set; }
        [ForeignKey("ChromosomeId")]
        [InverseProperty("Gene")]
        public virtual Chromosome Chromosome { get; set; }
        [ForeignKey("HumanGenomeAssemblyId")]
        [InverseProperty("Gene")]
        public virtual HumanGenomeAssembly HumanGenomeAssembly { get; set; }
    }
}
