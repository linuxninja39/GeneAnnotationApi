using System;

namespace GeneAnnotationApi.JsonModels
{
    public class AnnotationJsonModel: BaseJsonModel
    {
        public DateTime CreatedAt { get; set; }
        public string Note { get; set; }
    }
}