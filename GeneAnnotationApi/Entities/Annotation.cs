using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("annotation")]
    public partial class Annotation
    {
        public Annotation()
        {
            AnnotationAuthor = new HashSet<AnnotationAuthor>();
            AnnotationGene = new HashSet<AnnotationGene>();
            AnnotationGeneVariant = new HashSet<AnnotationGeneVariant>();
            AnnotationGeneVariantLiterature = new HashSet<AnnotationGeneVariantLiterature>();
            AnnotationLiterature = new HashSet<AnnotationLiterature>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("app_user_id")]
        public int? AppUserId { get; set; }
//        [Column("created_at", TypeName = "datetime")]
        [Column("created_at", TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; }
        [Required]
        [Column("note")]
        public string Note { get; set; }

        [InverseProperty("Annotation")]
        public virtual ICollection<AnnotationAuthor> AnnotationAuthor { get; set; }
        [InverseProperty("Annotation")]
        public virtual ICollection<AnnotationGene> AnnotationGene { get; set; }
        [InverseProperty("Annotation")]
        public virtual ICollection<AnnotationGeneVariant> AnnotationGeneVariant { get; set; }
        [InverseProperty("Annotation")]
        public virtual ICollection<AnnotationGeneVariantLiterature> AnnotationGeneVariantLiterature { get; set; }
        [InverseProperty("Annotation")]
        public virtual ICollection<AnnotationLiterature> AnnotationLiterature { get; set; }
        [ForeignKey("AppUserId")]
        [InverseProperty("Annotation")]
        public virtual AppUser AppUser { get; set; }
    }
}
