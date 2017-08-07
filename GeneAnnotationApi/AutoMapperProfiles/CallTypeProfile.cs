using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class CallTypeProfile : Profile
    {
        public CallTypeProfile()
        {
            CreateMap<CallType, CallTypeDto>();
            CreateMap<CallTypeDto, CallType>();
        }
    }
}