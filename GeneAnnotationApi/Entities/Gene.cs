using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
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

        public int Id { get; set; }
        public string GeneNameExpansion { get; set; }
        public string KnownGeneFunction { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int? OmimId { get; set; }
        public string Refseq { get; set; }
        public string EnsembleId { get; set; }
        public string Ucsc { get; set; }

        public virtual ICollection<Accession> Accession { get; set; }
        public virtual ICollection<AlternateGeneName> AlternateGeneName { get; set; }
        public virtual ICollection<AnnotationGene> AnnotationGene { get; set; }
        public virtual ICollection<GeneLocation> GeneLocation { get; set; }
        public virtual ICollection<GeneName> GeneName { get; set; }
        public virtual ICollection<GeneOriginType> GeneOriginType { get; set; }
        public virtual ICollection<GeneVariant> GeneVariant { get; set; }
        public virtual ICollection<NameSynonym> NameSynonym { get; set; }
        public virtual ICollection<Symbol> Symbol { get; set; }
        public virtual ICollection<Synonym> Synonym { get; set; }
    }
}
