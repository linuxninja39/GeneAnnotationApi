using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("foreign_entity")]
    public class ForeignEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        
        [InverseProperty("ForeignEntity")]
        public virtual ICollection<ForeignIdentity> ForeignIdentities { get; set; }
    }
}