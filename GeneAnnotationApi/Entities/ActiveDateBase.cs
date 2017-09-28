using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    public class ActiveDateBase
    {
        [Required]
        [Column("active_date", TypeName = "datetime")]
        public DateTime ActiveDate { get; set; }
    }
}