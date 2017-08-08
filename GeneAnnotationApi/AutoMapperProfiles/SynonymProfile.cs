using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class SynonymProfile : Profile
    {
        public SynonymProfile()
        {
            CreateMap<Synonym, SynonymDto>();
            CreateMap<SynonymDto, Synonym>();
        }
    }
}