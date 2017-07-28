using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("human_genome_assembly")]
    public partial class HumanGenomeAssembly
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("hg")]
        public int? Hg { get; set; }
    }
}
