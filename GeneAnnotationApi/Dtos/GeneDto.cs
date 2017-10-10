using System;
using System.Collections.Generic;

namespace GeneAnnotationApi.Dtos
{
    public class GeneDto: BaseDto
    {
        public IList<SymbolDto> Symbol { get; set; }
        public IList<GeneNameDto> GeneName { get; set; }
        public List<SynonymDto> Synonym { get; set; }
        public string GeneNameExpansion { get;  set; }
        public string KnownFunction { get;  set; }
        public List<GeneLocationDto> GeneLocation { get; set; }
        public List<OriginTypeDto> Origin { get;  set; }
        public List<AnnotationDto> Annotation { get;  set; }
    }
}