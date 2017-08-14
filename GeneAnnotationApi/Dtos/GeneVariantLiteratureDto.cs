namespace GeneAnnotationApi.Dtos
{
    public class GeneVariantLiteratureDto
    {
        public int GeneVariantId { get; set; }
        public int LiteratureId { get; set; }
        public LiteratureDto Literature { get; set; }
        public AnnotationDto[] Annotation { get; set; }
    }
}