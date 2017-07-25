using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("gene_origin_type")]
    public partial class GeneOriginType
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("gene_id")]
        public int? GeneId { get; set; }
        [Required]
        [Column("origin_type_id")]
        public int? OriginTypeId { get; set; }

        [ForeignKey("GeneId")]
        [InverseProperty("GeneOriginType")]
        public virtual Gene Gene { get; set; }
        [ForeignKey("OriginTypeId")]
        [InverseProperty("GeneOriginType")]
        public virtual OriginType OriginType { get; set; }
    }
}
