using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class OriginTypeProfile : Profile
    {
        public OriginTypeProfile()
        {
            CreateMap<OriginType, OriginTypeDto>();
        }
    }
}