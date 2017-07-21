using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class Author
    {
        public Author()
        {
            AnnotationAuthor = new HashSet<AnnotationAuthor>();
            AuthorLiterature = new HashSet<AuthorLiterature>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AnnotationAuthor> AnnotationAuthor { get; set; }
        public virtual ICollection<AuthorLiterature> AuthorLiterature { get; set; }
    }
}
