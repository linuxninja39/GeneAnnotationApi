using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneAnnotationApi.Entities
{
    [Table("app_user")]
    public partial class AppUser
    {
        public AppUser()
        {
            Annotation = new HashSet<Annotation>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(250)")]
        public string Name { get; set; }

        [InverseProperty("AppUser")]
        public virtual ICollection<Annotation> Annotation { get; set; }
    }
}
