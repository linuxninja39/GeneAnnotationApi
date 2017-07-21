using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class OriginType
    {
        public OriginType()
        {
            GeneOriginType = new HashSet<GeneOriginType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GeneOriginType> GeneOriginType { get; set; }
    }
}
