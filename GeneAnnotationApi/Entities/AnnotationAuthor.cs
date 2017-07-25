using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("annotation_author")]
    public partial class AnnotationAuthor
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("annotation_id")]
        public int? AnnotationId { get; set; }
        [Required]
        [Column("author_id")]
        public int? AuthorId { get; set; }

        [ForeignKey("AnnotationId")]
        [InverseProperty("AnnotationAuthor")]
        public virtual Annotation Annotation { get; set; }
        [ForeignKey("AuthorId")]
        [InverseProperty("AnnotationAuthor")]
        public virtual Author Author { get; set; }
    }
}
