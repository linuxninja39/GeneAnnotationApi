using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("annotation_literature")]
    public partial class AnnotationLiterature
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("annotation_id")]
        public int? AnnotationId { get; set; }
        [Required]
        [Column("literature_id")]
        public int? LiteratureId { get; set; }

        [ForeignKey("AnnotationId")]
        [InverseProperty("AnnotationLiterature")]
        public virtual Annotation Annotation { get; set; }
        [ForeignKey("LiteratureId")]
        [InverseProperty("AnnotationLiterature")]
        public virtual Literature Literature { get; set; }
    }
}
