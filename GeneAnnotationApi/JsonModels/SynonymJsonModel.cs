using System;

namespace GeneAnnotationApi.JsonModels
{
    public class SynonymJsonModel: BaseJsonModel
    {
        public GeneJsonModel Gene { get; set; }
        public string Synonym;
        public DateTime ActiveDate { get; set; }
    }
}