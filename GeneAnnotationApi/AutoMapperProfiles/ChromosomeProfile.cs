using AutoMapper;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class ChromosomeProfile : Profile
    {
        public ChromosomeProfile()
        {
            CreateMap<Chromosome, ChromosomeJsonModel>();
        }
    }
}