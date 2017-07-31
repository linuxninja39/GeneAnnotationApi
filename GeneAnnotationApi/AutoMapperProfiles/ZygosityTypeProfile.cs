using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class ZygosityTypeProfile : Profile
    {
        public ZygosityTypeProfile()
        {
            CreateMap<ZygosityType, ZygosityTypeDto>();
        }
    }
}