using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("gene_var_lit_disorder")]
    public partial class GeneVarLitDisorder
    {
        [Column("gene_var_lit_id")]
        public int GeneVarLitId { get; set; }
        [Column("disorder_id")]
        public int DisorderId { get; set; }

        [ForeignKey("DisorderId")]
        [InverseProperty("GeneVarLitDisorder")]
        public virtual Disorder Disorder { get; set; }
        [ForeignKey("GeneVarLitId")]
        [InverseProperty("GeneVarLitDisorder")]
        public virtual GeneVariantLiterature GeneVarLit { get; set; }
    }
}
