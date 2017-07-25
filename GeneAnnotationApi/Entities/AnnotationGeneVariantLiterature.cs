using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("annotation_gene_variant_literature")]
    public partial class AnnotationGeneVariantLiterature
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("annotation_id")]
        public int? AnnotationId { get; set; }
        [Required]
        [Column("gene_variant_literature_id")]
        public int? GeneVariantLiteratureId { get; set; }

        [ForeignKey("AnnotationId")]
        [InverseProperty("AnnotationGeneVariantLiterature")]
        public virtual Annotation Annotation { get; set; }
        [ForeignKey("GeneVariantLiteratureId")]
        [InverseProperty("AnnotationGeneVariantLiterature")]
        public virtual GeneVariantLiterature GeneVariantLiterature { get; set; }
    }
}
