using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class GeneNameProfile : Profile
    {
        public GeneNameProfile()
        {
            CreateMap<GeneName, GeneNameJsonModel>();
        }
    }
}