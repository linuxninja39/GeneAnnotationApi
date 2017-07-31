using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class GeneNameProfile : Profile
    {
        public GeneNameProfile()
        {
            CreateMap<GeneName, GeneNameDto>();
        }
    }
}