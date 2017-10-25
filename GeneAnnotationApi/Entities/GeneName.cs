using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("gene_name")]
    public partial class GeneName: ActiveDateBase
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("gene_id")]
        public int? GeneId { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(250)")]
        public string Name { get; set; }
        [Column("gene_name_expantion")]
        public string GeneNameExpansion { get; set; }
            

        [ForeignKey("GeneId")]
        [InverseProperty("GeneName")]
        public virtual Gene Gene { get; set; }
    }
}
