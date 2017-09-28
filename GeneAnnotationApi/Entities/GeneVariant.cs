using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("gene_variant")]
    public partial class GeneVariant
    {
        public GeneVariant()
        {
            AnnotationGeneVariant = new HashSet<AnnotationGeneVariant>();
            GeneVariantLiterature = new HashSet<GeneVariantLiterature>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("zygosity_type_id")]
        public int ZygosityTypeId { get; set; }
        [Required]
        [Column("variant_type_id")]
        public int VariantTypeId { get; set; }
        [Required]
        [Column("start")]
        public int Start { get; set; }
        [Required]
        [Column("end")]
        public int End { get; set; }
        [Column("coding_change", TypeName = "varchar(100)")]
        public string CodeingChange { get; set; }

        [InverseProperty("GeneVariant")]
        public virtual ICollection<AnnotationGeneVariant> AnnotationGeneVariant { get; set; }
        [InverseProperty("GeneVariant")]
        public virtual ICollection<GeneVariantLiterature> GeneVariantLiterature { get; set; }
        [InverseProperty("GeneVariant")]
        public virtual ICollection<CallTypeGeneVariant> CallTypeGeneVariants { get; set; }
        [ForeignKey("GeneId")]
        [InverseProperty("GeneVariant")]
        public virtual Gene Gene { get; set; }
        [ForeignKey("VariantTypeId")]
        [InverseProperty("GeneVariant")]
        public virtual VariantType VariantType { get; set; }
        [ForeignKey("ZygosityTypeId")]
        [InverseProperty("GeneVariant")]
        public virtual ZygosityType ZygosityType { get; set; }
    }
}
