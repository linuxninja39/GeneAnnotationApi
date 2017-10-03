using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class SymbolProfile : Profile
    {
        public SymbolProfile()
        {
            CreateMap<Symbol, SymbolDto>();
            CreateMap<SymbolDto, Symbol>()
                .ForMember(entity => entity.Gene, opt => opt.Ignore())
                .ForMember(entity => entity.GeneId, opt => opt.Ignore())
                ;
        }
    }
}