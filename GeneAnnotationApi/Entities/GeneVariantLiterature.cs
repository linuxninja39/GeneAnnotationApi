using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class GeneVariantLiterature
    {
        public GeneVariantLiterature()
        {
            AnnotationGeneVariantLiterature = new HashSet<AnnotationGeneVariantLiterature>();
            GeneVarLitDisorder = new HashSet<GeneVarLitDisorder>();
        }

        public int Id { get; set; }
        public int GeneVariantId { get; set; }
        public int? LiteratureId { get; set; }
        public bool? SupportsPathogenicity { get; set; }

        public virtual ICollection<AnnotationGeneVariantLiterature> AnnotationGeneVariantLiterature { get; set; }
        public virtual ICollection<GeneVarLitDisorder> GeneVarLitDisorder { get; set; }
        public virtual GeneVariant GeneVariant { get; set; }
        public virtual Literature Literature { get; set; }
    }
}
