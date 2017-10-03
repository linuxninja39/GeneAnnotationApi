using System.Collections.Generic;

namespace GeneAnnotationApi.Dtos
{
    public class LiteratureDto: BaseDto
    {
        public string Title;
        public string Url;
        public AuthorDto[] Authors;
        public AnnotationDto[] Annotations;
        public string PubMedId { get; set; }
        public GeneVariantLiteratureDto[] GeneVariantLiteratures { get; set; }
    }
}