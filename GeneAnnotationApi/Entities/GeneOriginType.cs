using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class GeneOriginType
    {
        public int Id { get; set; }
        public int? GeneId { get; set; }
        public int? OriginTypeId { get; set; }

        public virtual Gene Gene { get; set; }
        public virtual OriginType OriginType { get; set; }
    }
}
