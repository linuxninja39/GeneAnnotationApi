using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUserDto, AppUser>()
                .ForMember(dest => dest.Annotation, opt => opt.Ignore())
                .ForMember(dest => dest.GeneVariantLiteratures, opt => opt.Ignore())
                ;
        }
    }
}