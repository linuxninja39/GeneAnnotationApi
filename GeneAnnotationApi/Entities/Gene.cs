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
            AnnotationGene = new HashSet<AnnotationGene>();
            GeneLocation = new HashSet<GeneLocation>();
            GeneName = new HashSet<GeneName>();
            GeneOriginType = new HashSet<GeneOriginType>();
            NameSynonym = new HashSet<NameSynonym>();
            Symbol = new HashSet<Symbol>();
            Synonym = new HashSet<Synonym>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("known_function", TypeName = "varchar(2000)")]
        public string KnownFunction { get; set; }
  

        [InverseProperty("Gene")]
        public virtual ICollection<Accession> Accession { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<AnnotationGene> AnnotationGene { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<GeneLocation> GeneLocation { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<GeneName> GeneName { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<GeneOriginType> GeneOriginType { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<NameSynonym> NameSynonym { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<Symbol> Symbol { get; set; }
        [InverseProperty("Gene")]
        public virtual ICollection<Synonym> Synonym { get; set; }
    }
}
