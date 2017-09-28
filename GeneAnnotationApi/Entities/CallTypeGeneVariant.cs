using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("call_type_gene_variant")]
    public partial class CallTypeGeneVariant: ActiveDateBase
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("call_type_id")]
        public int CallTypeId { get; set; }
        [Column("gene_variant_id")]
        public int GeneVariantId { get; set; }
        
        
        [ForeignKey("CallTypeId")]
        [InverseProperty("CallTypeGeneVariants")]
        public virtual CallType CallType { get; set; }
        [ForeignKey("GeneVariantId")]
        [InverseProperty("CallTypeGeneVariants")]
        public virtual GeneVariant GeneVariant { get; set; }
    }
}
