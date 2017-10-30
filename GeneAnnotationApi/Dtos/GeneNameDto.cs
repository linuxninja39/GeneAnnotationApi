using System;

namespace GeneAnnotationApi.Dtos
{
    public class GeneNameDto: BaseDto
    {
        public string Name { get; set; }
        public DateTime ActiveDate { get; set; }
        public string GeneNameExpansion { get; set; }
    }
}