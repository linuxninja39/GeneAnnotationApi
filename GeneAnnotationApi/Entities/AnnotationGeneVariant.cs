using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("annotation_gene_variant")]
    public partial class AnnotationGeneVariant
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("annotation_id")]
        public int? AnnotationId { get; set; }
        [Required]
        [Column("gene_variant_id")]
        public int? GeneVariantId { get; set; }

        [ForeignKey("AnnotationId")]
        [InverseProperty("AnnotationGeneVariant")]
        public virtual Annotation Annotation { get; set; }
        [ForeignKey("GeneVariantId")]
        [InverseProperty("AnnotationGeneVariant")]
        public virtual GeneVariant GeneVariant { get; set; }
    }
}
