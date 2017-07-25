using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("gene_location")]
    public partial class GeneLocation
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("gene_id")]
        public int GeneId { get; set; }
        [Required]
        [Column("chr", TypeName = "varchar(5)")]
        public string Chr { get; set; }
        [Column("start")]
        public int Start { get; set; }
        [Column("end")]
        public int End { get; set; }
        [Required]
        [Column("locus", TypeName = "varchar(20)")]
        public string Locus { get; set; }
        [Column("hg_version")]
        public int HgVersion { get; set; }

        [ForeignKey("GeneId")]
        [InverseProperty("GeneLocation")]
        public virtual Gene Gene { get; set; }
    }
}
