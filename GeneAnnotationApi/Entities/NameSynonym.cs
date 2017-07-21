using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class NameSynonym
    {
        public int Id { get; set; }
        public int? GeneId { get; set; }
        public string Synonum { get; set; }
        public DateTime ActiveDate { get; set; }

        public virtual Gene Gene { get; set; }
    }
}
