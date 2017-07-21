using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class AnnotationGeneVariant
    {
        public int Id { get; set; }
        public int? AnnotationId { get; set; }
        public int? GeneVariantId { get; set; }

        public virtual Annotation Annotation { get; set; }
        public virtual GeneVariant GeneVariant { get; set; }
    }
}
