using System;

namespace GeneAnnotationApi.JsonModels
{
    public class GeneJsonModel: BaseJsonModel
    {
        public SymbolJsonModel[] Symbol { get; set; }
        public ChromosomeJsonModel Chromosome { get; set; }
        public GeneNameJsonModel[] GeneName { get; set; }
        public string[] Synonym { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string GeneNameExpansion { get;  set; }
        public string KnownGeneFunction { get;  set; }
        public GeneLocationJsonModel[] GeneLocation { get; set; }
        public OriginJsonType[] Origin { get;  set; }
        public GeneVariantJsonModel[] GeneVariant { get;  set; }
        public AnnotationJsonModel[] AnnotationGene { get;  set; }
    }
}