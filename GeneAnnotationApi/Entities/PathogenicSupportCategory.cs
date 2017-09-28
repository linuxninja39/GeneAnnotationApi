using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("pathogenic_support_category")]
    public partial class PathogenicSupportCategory
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(250)")]
        public string Name { get; set; }

        [InverseProperty("PathogenicSupportCategory")]
        public virtual ICollection<GeneVariantLiterature> GeneVariantLiteratures { get; set; }
    }
}
