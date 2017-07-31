namespace GeneAnnotationApi.JsonModels
{
    public class GeneVariantJsonModel: BaseJsonModel
    {
        public GeneJsonModel Gene { get; set; }
        public VariantTypeJsonModel VariantType { get; set; }
        public CallTypeJsonModel CallType { get; set; }
        public AnnotationJsonModel[] Annotation { get; set; }
        public LiteratureJsonModel[]  Literature { get; set; }
        public ZygosityTypeJsonModel ZygosityType { get; set; }
    }
}