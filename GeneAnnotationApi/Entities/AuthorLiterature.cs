using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class AuthorLiterature
    {
        public int Id { get; set; }
        public int? AuthorId { get; set; }
        public int? LiteratureId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Literature Literature { get; set; }
    }
}
