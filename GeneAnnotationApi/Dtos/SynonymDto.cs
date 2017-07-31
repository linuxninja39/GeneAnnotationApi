using System;

namespace GeneAnnotationApi.Dtos
{
    public class SynonymDto: BaseDto
    {
        public string Name;
        public DateTime ActiveDate { get; set; }
    }
}