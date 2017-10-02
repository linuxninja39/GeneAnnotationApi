using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class AnnotationProfile : Profile
    {
        public AnnotationProfile()
        {
            CreateMap<Annotation, AnnotationDto>()
                ;
            CreateMap<AnnotationDto, Annotation>()
                .ForMember(dest => dest.AnnotationAuthor, opt => opt.Ignore())
                .ForMember(dest => dest.AnnotationGene, opt => opt.Ignore())
                .ForMember(dest => dest.AnnotationGeneVariant, opt => opt.Ignore())
                .ForMember(dest => dest.AnnotationGeneVariantLiterature, opt => opt.Ignore())
                .ForMember(dest => dest.AnnotationLiterature, opt => opt.Ignore())
                ;
        }
    }
}