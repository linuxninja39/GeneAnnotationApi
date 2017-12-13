using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("name_synonym")]
    public partial class NameSynonym
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("gene_id")]
        public int? GeneId { get; set; }
        [Required]
        [Column("synonum", TypeName = "varchar(250)")]
        public string Synonum { get; set; }
        [Column("active_date")]
        public DateTime ActiveDate { get; set; }

        [ForeignKey("GeneId")]
        [InverseProperty("NameSynonym")]
        public virtual Gene Gene { get; set; }
    }
}
