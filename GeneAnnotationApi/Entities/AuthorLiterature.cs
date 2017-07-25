using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("author_literature")]
    public partial class AuthorLiterature
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("author_id")]
        public int? AuthorId { get; set; }
        [Required]
        [Column("literature_id")]
        public int? LiteratureId { get; set; }

        [ForeignKey("AuthorId")]
        [InverseProperty("AuthorLiterature")]
        public virtual Author Author { get; set; }
        [ForeignKey("LiteratureId")]
        [InverseProperty("AuthorLiterature")]
        public virtual Literature Literature { get; set; }
    }
}
