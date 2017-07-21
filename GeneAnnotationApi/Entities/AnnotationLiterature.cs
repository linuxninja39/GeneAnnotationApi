using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class AnnotationLiterature
    {
        public int Id { get; set; }
        public int? AnnotationId { get; set; }
        public int? LiteratureId { get; set; }

        public virtual Annotation Annotation { get; set; }
        public virtual Literature Literature { get; set; }
    }
}
