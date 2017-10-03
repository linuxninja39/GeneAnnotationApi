using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class OriginTypeProfile : Profile
    {
        public OriginTypeProfile()
        {
            CreateMap<OriginType, OriginTypeDto>();
            CreateMap<OriginTypeDto, OriginType>()
                .ForMember(
                    originTypeEntity => originTypeEntity.GeneOriginType,
                    opt => opt.Ignore()
                    )
                ;
        }
    }
}