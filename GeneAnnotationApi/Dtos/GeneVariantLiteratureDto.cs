using System;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Dtos
{
    public class GeneVariantLiteratureDto: BaseDto
    {
        public GeneVariantDto GeneVariant { get; set; }
        public LiteratureDto Literature { get; set; }
        public AnnotationDto[] Annotations { get; set; }
        public AppUserDto AppUser { get; set; }
        public PathogenicSupportCategoryDto PathogenicSupportCategory { get; set; }
        public DateTime AddedAt { get; set; }
    }
}