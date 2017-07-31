using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("synonym")]
    public partial class Synonym
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("gene_id")]
        public int? GeneId { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(250)")]
        public string Name { get; set; }
        [Column("active_date", TypeName = "datetime")]
        public DateTime ActiveDate { get; set; }

        [ForeignKey("GeneId")]
        [InverseProperty("Synonym")]
        public virtual Gene Gene { get; set; }
    }
}
