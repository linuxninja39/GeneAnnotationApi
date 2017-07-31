using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class AnnotationProfile : Profile
    {
        public AnnotationProfile()
        {
            CreateMap<Annotation, AnnotationDto>();
        }
    }
}