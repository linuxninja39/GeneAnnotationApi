using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class VariantType
    {
        public VariantType()
        {
            GeneVariant = new HashSet<GeneVariant>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GeneVariant> GeneVariant { get; set; }
    }
}
