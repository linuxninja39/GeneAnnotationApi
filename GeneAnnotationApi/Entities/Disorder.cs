using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class Disorder
    {
        public Disorder()
        {
            GeneVarLitDisorder = new HashSet<GeneVarLitDisorder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GeneVarLitDisorder> GeneVarLitDisorder { get; set; }
    }
}
