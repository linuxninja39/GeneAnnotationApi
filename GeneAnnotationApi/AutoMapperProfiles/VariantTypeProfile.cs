using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class VariantTypeProfile : Profile
    {
        public VariantTypeProfile()
        {
            CreateMap<VariantType, VariantTypeDto>();
            CreateMap<VariantTypeDto, VariantType>()
                .ForMember(entity => entity.GeneVariant, opt => opt.Ignore())
                ;
        }
    }
}