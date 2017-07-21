using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class Literature
    {
        public Literature()
        {
            AnnotationLiterature = new HashSet<AnnotationLiterature>();
            AuthorLiterature = new HashSet<AuthorLiterature>();
            GeneVariantLiterature = new HashSet<GeneVariantLiterature>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public virtual ICollection<AnnotationLiterature> AnnotationLiterature { get; set; }
        public virtual ICollection<AuthorLiterature> AuthorLiterature { get; set; }
        public virtual ICollection<GeneVariantLiterature> GeneVariantLiterature { get; set; }
    }
}
