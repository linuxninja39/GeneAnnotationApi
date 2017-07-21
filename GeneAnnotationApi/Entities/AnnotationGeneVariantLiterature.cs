using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class AnnotationGeneVariantLiterature
    {
        public int Id { get; set; }
        public int? AnnotationId { get; set; }
        public int? GeneVariantLiteratureId { get; set; }

        public virtual Annotation Annotation { get; set; }
        public virtual GeneVariantLiterature GeneVariantLiterature { get; set; }
    }
}
