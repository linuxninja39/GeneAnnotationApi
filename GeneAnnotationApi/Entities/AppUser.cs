using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class AppUser
    {
        public AppUser()
        {
            Annotation = new HashSet<Annotation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Annotation> Annotation { get; set; }
    }
}
