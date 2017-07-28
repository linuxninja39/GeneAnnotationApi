using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class AnnotationGeneVariantProfile: Profile
    {
        public AnnotationGeneVariantProfile()
        {
            CreateMap<AnnotationGeneVariant[], AnnotationJsonModel[]>();
        }
    }
}