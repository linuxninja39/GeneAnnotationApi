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
            CreateMap<SynonymDto, Synonym>()
                .ForMember(entity => entity.Gene, opt => opt.Ignore())
                .ForMember(entity => entity.GeneId, opt => opt.Ignore())
                ;
        }
    }
}