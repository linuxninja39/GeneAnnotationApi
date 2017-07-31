using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class VariantTypeProfile : Profile
    {
        public VariantTypeProfile()
        {
            CreateMap<VariantType, VariantTypeDto>();
        }
    }
}