using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class LiteratureProfile : Profile
    {
        public LiteratureProfile()
        {
            CreateMap<Literature, LiteratureJsonModel>();
        }
    }
}