using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class ZygosityTypeProfile : Profile
    {
        public ZygosityTypeProfile()
        {
            CreateMap<ZygosityType, ZygosityTypeJsonModel>();
        }
    }
}