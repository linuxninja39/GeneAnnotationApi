using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("alternate_gene_name")]
    public partial class AlternateGeneName
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("gene_id")]
        public int GeneId { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [ForeignKey("GeneId")]
        [InverseProperty("AlternateGeneName")]
        public virtual Gene Gene { get; set; }
    }
}
