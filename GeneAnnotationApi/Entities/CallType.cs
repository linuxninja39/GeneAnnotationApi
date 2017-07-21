using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class CallType
    {
        public CallType()
        {
            GeneVariant = new HashSet<GeneVariant>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GeneVariant> GeneVariant { get; set; }
    }
}
