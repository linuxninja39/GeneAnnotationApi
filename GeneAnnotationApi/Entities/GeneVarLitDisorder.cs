using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class GeneVarLitDisorder
    {
        public int GeneVarLitId { get; set; }
        public int DisorderId { get; set; }

        public virtual Disorder Disorder { get; set; }
        public virtual GeneVariantLiterature GeneVarLit { get; set; }
    }
}
