using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class SymbolProfile : Profile
    {
        public SymbolProfile()
        {
            CreateMap<Symbol, SymbolDto>();
        }
    }
}