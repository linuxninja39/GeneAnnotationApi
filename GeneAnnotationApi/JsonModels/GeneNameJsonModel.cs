using System;

namespace GeneAnnotationApi.JsonModels
{
    public class GeneNameJsonModel: BaseJsonModel
    {
        public GeneJsonModel Gene { get; set; }
        public string Name { get; set; }
        public DateTime ActiveDate { get; set; }
    }
}