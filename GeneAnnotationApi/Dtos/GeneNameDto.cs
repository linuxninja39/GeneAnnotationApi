using System;

namespace GeneAnnotationApi.Dtos
{
    public class GeneNameDto: BaseDto
    {
        public GeneDto Gene { get; set; }
        public string Name { get; set; }
        public DateTime ActiveDate { get; set; }
    }
}