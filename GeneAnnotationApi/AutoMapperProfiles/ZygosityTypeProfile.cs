using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class ZygosityTypeProfile : Profile
    {
        public ZygosityTypeProfile()
        {
            CreateMap<ZygosityType, ZygosityTypeDto>();
            CreateMap<ZygosityTypeDto, ZygosityType>();
        }
    }
}