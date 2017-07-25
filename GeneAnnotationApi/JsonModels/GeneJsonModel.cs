using System;

namespace GeneAnnotationApi.JsonModels
{
    public class GeneJsonModel
    {
        public SymbolJsonModel[] Symbol { get; set; }
        public HumanGenomeAssemblyJsonModel HumanGenomeAssembly { get; set; }
        public ChromosomeJsonModel Chromosome { get; set; }
        public string Name { get; set; }
        public string[] PreviousNames { get; set; }
        public string[] Synonyms { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string GeneNameExpansion { get;  set; }
        public string KnownGeneFunction { get;  set; }
        public string Locus { get;  set; }
        public int Start { get;  set; }
        public int End { get;  set; }
        public string Origin { get;  set; }
        public GeneVariantJsonModel[] Variants { get;  set; }
        public AnnotationJsonModel[] Annotations { get;  set; }
    }
}