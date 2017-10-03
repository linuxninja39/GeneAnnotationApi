using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class PathogenicSupportCategoryProfile : Profile
    {
        public PathogenicSupportCategoryProfile()
        {
            CreateMap<PathogenicSupportCategory, PathogenicSupportCategoryDto>();
            CreateMap<PathogenicSupportCategoryDto, PathogenicSupportCategory>()
                .ForMember(entity => entity.GeneVariantLiteratures, opt => opt.Ignore())
                ;
        }
    }
}