using System.Collections.Generic;

namespace GeneAnnotationApi.Dtos
{
    public class LiteratureDto: BaseDto
    {
        public string Title;
        public string Url;
        public List<AuthorDto> Author;
        public List<AnnotationDto> Annotation;
    }
}