using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("zygosity_type")]
    public partial class ZygosityType
    {
        public ZygosityType()
        {
            GeneVariant = new HashSet<GeneVariant>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [InverseProperty("ZygosityType")]
        public virtual ICollection<GeneVariant> GeneVariant { get; set; }
    }
}
