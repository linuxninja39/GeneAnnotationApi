namespace GeneAnnotationApi.Dtos
{
    public class GeneVariantDto: BaseDto
    {
        public GeneDto Gene { get; set; }
        public VariantTypeDto VariantType { get; set; }
        public CallTypeDto CallType { get; set; }
        public AnnotationDto[] Annotation { get; set; }
        public LiteratureDto[]  Literature { get; set; }
        public ZygosityTypeDto ZygosityType { get; set; }
    }
}