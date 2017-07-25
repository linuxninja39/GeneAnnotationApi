using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("call_type")]
    public partial class CallType
    {
        public CallType()
        {
            GeneVariant = new HashSet<GeneVariant>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [InverseProperty("CallType")]
        public virtual ICollection<GeneVariant> GeneVariant { get; set; }
    }
}
