using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class OriginTypeProfile : Profile
    {
        public OriginTypeProfile()
        {
            CreateMap<OriginType, OriginTypeJsonModel>();
        }
    }
}