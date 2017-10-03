namespace GeneAnnotationApi.Dtos
{
    public class VariantTypeDto: BaseDto
    {
        public string Name { get; set; }
        public VariantTypeDto Parent { get; set; }
        public VariantTypeDto[] Children { get; set; }
    }
}