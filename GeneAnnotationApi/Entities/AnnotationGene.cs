using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("annotation_gene")]
    public partial class AnnotationGene
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("annotation_id")]
        public int? AnnotationId { get; set; }
        [Required]
        [Column("gene_id")]
        public int? GeneId { get; set; }

        [ForeignKey("AnnotationId")]
        [InverseProperty("AnnotationGene")]
        public virtual Annotation Annotation { get; set; }
        [ForeignKey("GeneId")]
        [InverseProperty("AnnotationGene")]
        public virtual Gene Gene { get; set; }
    }
}
