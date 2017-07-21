using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class GeneLocation
    {
        public int Id { get; set; }
        public int GeneId { get; set; }
        public string Chr { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string Locus { get; set; }
        public int HgVersion { get; set; }

        public virtual Gene Gene { get; set; }
    }
}
