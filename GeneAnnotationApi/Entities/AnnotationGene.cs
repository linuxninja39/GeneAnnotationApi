using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class AnnotationGene
    {
        public int Id { get; set; }
        public int? AnnotationId { get; set; }
        public int? GeneId { get; set; }

        public virtual Annotation Annotation { get; set; }
        public virtual Gene Gene { get; set; }
    }
}
