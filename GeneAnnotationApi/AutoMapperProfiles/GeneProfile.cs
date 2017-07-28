using AutoMapper;
using GeneAnnotationApi.AutoMapperProfiles.CustomResolvers;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class GeneProfile: Profile
    {
        public GeneProfile()
        {
            var originResolver = new OriginResolver();
            CreateMap<Gene, GeneJsonModel>()
                .ForMember(
                    destinationMember => destinationMember.Origin,
                    opt => opt.ResolveUsing(originResolver)
                )
                ;
        }
    
    }
    
}