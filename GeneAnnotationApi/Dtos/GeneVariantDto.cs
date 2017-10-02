namespace GeneAnnotationApi.Dtos
{
    public class GeneVariantDto: BaseDto
    {
        public int VariantTypeId { get; set; }
        public VariantTypeDto VariantType { get; set; }
        public AnnotationDto[] Annotation { get; set; }
        public GeneVariantLiteratureDto[]  GeneVariantLiterature { get; set; }
        public int ZygosityTypeId { get; set; }
        public ZygosityTypeDto ZygosityType { get; set; }
        public CallTypeDto[] CallTypes { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string CodingChange { get; set; }
    }
}