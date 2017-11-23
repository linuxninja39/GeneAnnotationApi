using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("chromosome")]
    public partial class Chromosome
    {
        public Chromosome()
        {
            GeneLocations = new HashSet<GeneLocation>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(2)")]
        public string Name { get; set; }

        [InverseProperty("Chromosome")]
        public virtual ICollection<GeneLocation> GeneLocations { get; set; }
    }
}
