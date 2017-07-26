using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class GeneVariantProfile: Profile
    {
        public GeneVariantProfile()
        {
            CreateMap<GeneVariant, GeneVariantJsonModel>();
        }
    }
}