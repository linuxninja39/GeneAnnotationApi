using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("symbol")]
    public partial class Symbol: ActiveDateBase
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("gene_id")]
        public int? GeneId { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(10)")]
        public string Name { get; set; }

        [ForeignKey("GeneId")]
        [InverseProperty("Symbol")]
        public virtual Gene Gene { get; set; }
    }
}
