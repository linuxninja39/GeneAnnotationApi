using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
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

        public int Id { get; set; }
        public int? AppUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Annotation1 { get; set; }

        public virtual ICollection<AnnotationAuthor> AnnotationAuthor { get; set; }
        public virtual ICollection<AnnotationGene> AnnotationGene { get; set; }
        public virtual ICollection<AnnotationGeneVariant> AnnotationGeneVariant { get; set; }
        public virtual ICollection<AnnotationGeneVariantLiterature> AnnotationGeneVariantLiterature { get; set; }
        public virtual ICollection<AnnotationLiterature> AnnotationLiterature { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
