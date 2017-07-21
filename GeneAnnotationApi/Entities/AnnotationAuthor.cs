using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class AnnotationAuthor
    {
        public int Id { get; set; }
        public int? AnnotationId { get; set; }
        public int? AuthorId { get; set; }

        public virtual Annotation Annotation { get; set; }
        public virtual Author Author { get; set; }
    }
}
