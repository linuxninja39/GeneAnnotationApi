using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [InverseProperty("VariantType")]
        public virtual ICollection<GeneVariant> GeneVariant { get; set; }
    }
}
