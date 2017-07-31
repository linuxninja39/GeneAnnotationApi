using System;

namespace GeneAnnotationApi.Dtos
{
    public class AnnotationDto: BaseDto
    {
        public AppUserDto AppUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Note { get; set; }
    }
}