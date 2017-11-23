using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class ChromosomeProfile : Profile
    {
        public ChromosomeProfile()
        {
            CreateMap<Chromosome, ChromosomeDto>();
            CreateMap<ChromosomeDto, Chromosome>()
                .ForMember(
                    chromosome => chromosome.GeneLocations,
                    opt => opt.Ignore()
                    )
                ;
        }
    }
}