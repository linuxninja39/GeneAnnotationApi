using System;
using GeneAnnotationApi.Dtos;

namespace GeneAnnotationApi.JsonModels
{
    public class SynonymDto: BaseDto
    {
        public string Name;
        public DateTime ActiveDate { get; set; }
    }
}