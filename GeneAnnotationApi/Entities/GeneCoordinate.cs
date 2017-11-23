
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("gene_coordinate")]
    public class GeneCoordinate
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("gene_location_id")]
        public int GeneLocationId { get; set; }
        [Column("start")]
        public int Start { get; set; }
        [Column("end")]
        public int End { get; set; }
        
        [ForeignKey("GeneLocationId")]
        [InverseProperty("GeneCoordinates")]
        public virtual GeneLocation GeneLocation { get; set; }
        
        [InverseProperty("GeneCoordinate")]
        public virtual ICollection<ForeignIdentityGeneCoordinate> ForeignIdentityGeneCoordinates { get; set; }
    }
}