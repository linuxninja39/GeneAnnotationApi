using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class CallTypeProfile : Profile
    {
        public CallTypeProfile()
        {
            CreateMap<CallType, CallTypeJsonModel>();
        }
    }
}