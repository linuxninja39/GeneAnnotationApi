using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class GeneProfile: Profile
    {
        public GeneProfile()
        {
            CreateMap<Gene, GeneJsonModel>();
        }
    }
}