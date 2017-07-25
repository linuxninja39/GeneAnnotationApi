using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("disorder")]
    public partial class Disorder
    {
        public Disorder()
        {
            GeneVarLitDisorder = new HashSet<GeneVarLitDisorder>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [InverseProperty("Disorder")]
        public virtual ICollection<GeneVarLitDisorder> GeneVarLitDisorder { get; set; }
    }
}
