using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace GeneAnnotationApi.Entities
{
    [Table("variant_type")]
    public partial class VariantType
    {
        public VariantType()
        {
            GeneVariant = new HashSet<GeneVariant>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }
        [Column("parent_id")]
        public int? ParentId { get; set; }

        [InverseProperty("VariantType")]
        public virtual ICollection<GeneVariant> GeneVariant { get; set; }
        [InverseProperty("Parent")]
        public virtual IList<VariantType> Children { get; set; }
        
        // [InverseProperty("Children")]
        [ForeignKey("ParentId")]
        public virtual VariantType Parent { get; set; }
    }
}
