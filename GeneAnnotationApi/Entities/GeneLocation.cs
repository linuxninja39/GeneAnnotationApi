using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("gene_location")]
    public partial class GeneLocation
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("gene_id")]
        public int GeneId { get; set; }
        [Required]
        [Column("chromosome_id")]
        public int ChromosomeId { get; set; }
        [Column("locus", TypeName = "varchar(20)")]
        public string Locus { get; set; }
        [Column("hg_version")]
        public int HgVersion { get; set; }

        [ForeignKey("GeneId")]
        [InverseProperty("GeneLocations")]
        public virtual Gene Gene { get; set; }
        [ForeignKey("ChromosomeId")]
        [InverseProperty("GeneLocations")]
        public virtual Chromosome Chromosome { get; set; }
        
        [InverseProperty("GeneLocation")]
        public virtual ICollection<GeneCoordinate> GeneCoordinates { get; set; }
    }
}
