using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class GeneLocationProfile : Profile
    {
        public GeneLocationProfile()
        {
            CreateMap<GeneLocation, GeneLocationDto>();
        }
    }
}