using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Entities
{
    public partial class GeneVariant
    {
        public GeneVariant()
        {
            AnnotationGeneVariant = new HashSet<AnnotationGeneVariant>();
            GeneVariantLiterature = new HashSet<GeneVariantLiterature>();
        }

        public int Id { get; set; }
        public int GeneId { get; set; }
        public int ZygosityTypeId { get; set; }
        public int VariantTypeId { get; set; }
        public int CallTypeId { get; set; }

        public virtual ICollection<AnnotationGeneVariant> AnnotationGeneVariant { get; set; }
        public virtual ICollection<GeneVariantLiterature> GeneVariantLiterature { get; set; }
        public virtual CallType CallType { get; set; }
        public virtual Gene Gene { get; set; }
        public virtual VariantType VariantType { get; set; }
        public virtual ZygosityType ZygosityType { get; set; }
    }
}
