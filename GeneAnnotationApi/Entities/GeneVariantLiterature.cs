using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("gene_variant_literature")]
    public partial class GeneVariantLiterature
    {
        public GeneVariantLiterature()
        {
            AnnotationGeneVariantLiterature = new HashSet<AnnotationGeneVariantLiterature>();
            GeneVarLitDisorder = new HashSet<GeneVarLitDisorder>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("gene_variant_id")]
        public int GeneVariantId { get; set; }
        [Column("literature_id")]
        public int LiteratureId { get; set; }
        [Column("app_user_id")]
        public int AppUserId { get; set; }
        [Column("pathogenic_support_category_id")]
        public int PathogenicSupportCategoryId { get; set; }
        [Column("added_at")]
        public DateTime AddedAt { get; set; }
        

        [InverseProperty("GeneVariantLiterature")]
        public virtual ICollection<AnnotationGeneVariantLiterature> AnnotationGeneVariantLiterature { get; set; }
        [InverseProperty("GeneVarLit")]
        public virtual ICollection<GeneVarLitDisorder> GeneVarLitDisorder { get; set; }
        [ForeignKey("GeneVariantId")]
        [InverseProperty("GeneVariantLiterature")]
        public virtual GeneVariant GeneVariant { get; set; }
        [ForeignKey("LiteratureId")]
        [InverseProperty("GeneVariantLiteratures")]
        public virtual Literature Literature { get; set; }
        [ForeignKey("AppUserId")]
        [InverseProperty("GeneVariantLiteratures")]
        public virtual AppUser AppUser { get; set; }
        [ForeignKey("PathogenicSupportCategoryId")]
        [InverseProperty("GeneVariantLiteratures")]
        public virtual PathogenicSupportCategory PathogenicSupportCategory { get; set; }
    }
}
