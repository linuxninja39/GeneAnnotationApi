using System;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.Dtos
{
    public class AnnotationDto: BaseDto
    {
        public DateTime CreatedAt { get; set; }
        public string Note { get; set; }
    }
}