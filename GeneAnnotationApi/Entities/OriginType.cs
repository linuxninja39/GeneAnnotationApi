using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("origin_type")]
    public partial class OriginType
    {
        public OriginType()
        {
            GeneOriginType = new HashSet<GeneOriginType>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [InverseProperty("OriginType")]
        public virtual ICollection<GeneOriginType> GeneOriginType { get; set; }
    }
}
