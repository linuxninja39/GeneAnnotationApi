using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class GeneLocationProfile : Profile
    {
        public GeneLocationProfile()
        {
            CreateMap<GeneLocation, GeneLocationDto>();
            CreateMap<GeneLocationDto, GeneLocation>();
        }
    }
}