using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class SynonymProfile : Profile
    {
        public SynonymProfile()
        {
            CreateMap<Synonym, SynonymDto>();
        }
    }
}