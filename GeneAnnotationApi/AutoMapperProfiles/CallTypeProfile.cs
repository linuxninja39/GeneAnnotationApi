using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using Microsoft.CodeAnalysis;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class CallTypeProfile : Profile
    {
        public CallTypeProfile()
        {
            CreateMap<CallType, CallTypeDto>();
            CreateMap<CallTypeDto, CallType>()
                .ForMember(
                    callTypeDto => callTypeDto.CallTypeGeneVariants,
                    opt => opt.Ignore()
                    )
                ;
        }
    }
}