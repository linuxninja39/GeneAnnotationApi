using System;

namespace GeneAnnotationApi.Dtos
{
    public class SymbolDto: BaseDto
    {
        public string Name { get; set; }
        public DateTime ActiveDate { get; set; }
    }
}