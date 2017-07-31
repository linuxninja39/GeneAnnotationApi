using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.JsonModels;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class ChromosomeProfile : Profile
    {
        public ChromosomeProfile()
        {
            CreateMap<Chromosome, ChromosomeDto>();
        }
    }
}