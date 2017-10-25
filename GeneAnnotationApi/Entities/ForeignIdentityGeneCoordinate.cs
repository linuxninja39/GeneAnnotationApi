using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{    
    [Table("foreign_identity_gene_coordinate")]
    public class ForeignIdentityGeneCoordinate
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("foreign_identity_id")]
        public int ForeignIdentityId { get; set; }
        [Column("gene_coordinate_id")]
        public int GeneCoordinateId { get; set; }
        
        [ForeignKey("ForeignIdentityId")]
        [InverseProperty("ForeignIndentity")]
        public virtual ForeignIdentity ForeignIdentity{ get; set; }
        
        [ForeignKey("GeneCoordinateId")]
        [InverseProperty("GeneCoordinate")]
        public virtual GeneCoordinate GeneCoordinate { get; set; }
        
    }
}