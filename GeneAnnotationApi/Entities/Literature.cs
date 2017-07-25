using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("literature")]
    public partial class Literature
    {
        public Literature()
        {
            AnnotationLiterature = new HashSet<AnnotationLiterature>();
            AuthorLiterature = new HashSet<AuthorLiterature>();
            GeneVariantLiterature = new HashSet<GeneVariantLiterature>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("title", TypeName = "varchar(2000)")]
        public string Title { get; set; }
        [Required]
        [Column("url", TypeName = "varchar(2000)")]
        public string Url { get; set; }

        [InverseProperty("Literature")]
        public virtual ICollection<AnnotationLiterature> AnnotationLiterature { get; set; }
        [InverseProperty("Literature")]
        public virtual ICollection<AuthorLiterature> AuthorLiterature { get; set; }
        [InverseProperty("Literature")]
        public virtual ICollection<GeneVariantLiterature> GeneVariantLiterature { get; set; }
    }
}
