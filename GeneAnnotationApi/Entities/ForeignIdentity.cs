using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("foreign_indentity")]
    public class ForeignIdentity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("foreign_entity_id")]
        public int ForeignEntityId { get; set; }
        
        [ForeignKey("ForeignEntityId")]
        [InverseProperty("ForeignIdentities")]
        public virtual ForeignEntity ForeignEntity { get; set; }
        
        [InverseProperty("ForeignIdentity")]
        public virtual ICollection<ForeignIdentityGeneCoordinate> ForeignIdentityGeneCoordinates { get; set; }
        
    }
}