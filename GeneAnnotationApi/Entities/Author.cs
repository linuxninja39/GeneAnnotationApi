using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("author")]
    public partial class Author
    {
        public Author()
        {
            AnnotationAuthor = new HashSet<AnnotationAuthor>();
            AuthorLiterature = new HashSet<AuthorLiterature>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(250)")]
        public string Name { get; set; }

        [InverseProperty("Author")]
        public virtual ICollection<AnnotationAuthor> AnnotationAuthor { get; set; }
        [InverseProperty("Author")]
        public virtual ICollection<AuthorLiterature> AuthorLiterature { get; set; }
    }
}
