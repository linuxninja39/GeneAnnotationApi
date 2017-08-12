using System;
using System.ComponentModel.DataAnnotations;

namespace GeneAnnotationApi.Dtos
{
    public class AnnotationDto: BaseDto
    {
        [Required]
        public int AppUserId { get; set; }
        public AppUserDto AppUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Note { get; set; }
    }
}