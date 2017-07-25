using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class HumanGenomeAssemblyProfile : Profile
    {
        public HumanGenomeAssemblyProfile()
        {
            CreateMap<HumanGenomeAssembly, HumanGenomeAssemblyJsonModel>();
        }
    }
}