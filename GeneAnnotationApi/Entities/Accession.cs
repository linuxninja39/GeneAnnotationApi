using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class Accession
    {
        public int Id { get; set; }
        public int? GeneId { get; set; }
        public string AccessionNumber { get; set; }

        public virtual Gene Gene { get; set; }
    }
}
