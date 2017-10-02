
namespace GeneAnnotationApi.Dtos
{
    public class AuthorDto: BaseDto
    {
        public string Name { get; set; }
        public AnnotationDto[] Annotations { get; set; }
        public LiteratureDto[] Literatures { get; set; }
    }
}