using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class GeneNameProfile : Profile
    {
        public GeneNameProfile()
        {
            CreateMap<GeneName, GeneNameDto>();
            CreateMap<GeneNameDto, GeneName>();
        }
    }
}