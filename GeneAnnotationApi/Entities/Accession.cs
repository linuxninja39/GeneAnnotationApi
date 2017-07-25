using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("accession")]
    public partial class Accession
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("gene_id")]
        public int? GeneId { get; set; }
        [Required]
        [Column("accession_number", TypeName = "varchar(250)")]
        public string AccessionNumber { get; set; }

        [ForeignKey("GeneId")]
        [InverseProperty("Accession")]
        public virtual Gene Gene { get; set; }
    }
}
