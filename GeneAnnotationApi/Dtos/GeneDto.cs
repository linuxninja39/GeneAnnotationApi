using System;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.Dtos
{
    public class GeneDto: BaseDto
    {
        public SymbolDto[] Symbol { get; set; }
        public ChromosomeDto Chromosome { get; set; }
        public GeneNameDto[] GeneName { get; set; }
        public SynonymDto[] Synonym { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string GeneNameExpansion { get;  set; }
        public string KnownGeneFunction { get;  set; }
        public GeneLocationDto[] GeneLocation { get; set; }
        public OriginTypeDto[] Origin { get;  set; }
        public GeneVariantDto[] GeneVariant { get;  set; }
        public AnnotationDto[] Annotation { get;  set; }
    }
}