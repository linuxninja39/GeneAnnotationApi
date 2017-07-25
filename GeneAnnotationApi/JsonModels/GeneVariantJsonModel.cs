namespace GeneAnnotationApi.JsonModels
{
    public class GeneVariantJsonModel
    {
        public GeneJsonModel Gene { get; set; }
        public GeneVariantZygosityJsonType ZygosityJson { get; set; }
        public GeneVariantJsonType JsonType { get; set; }
        public GeneVariantCallJsonType CallJson { get; set; }
        public AnnotationJsonModel Annotations { get; set; }
        public GeneVariantLiteratureJsonModel  Literatures { get; set; }
    }
}