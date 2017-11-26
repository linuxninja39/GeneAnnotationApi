using AutoMapper;
using GeneAnnotationApi.AutoMapperProfiles.CustomResolvers;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.AutoMapperProfiles
{
    public class GeneLocationProfile : Profile
    {
        public GeneLocationProfile()
        {
            CreateMap<GeneLocation, GeneLocationDto>()
                .ForMember(
                    geneLocationDto => geneLocationDto.Start,
                    opt => opt.ResolveUsing<GeneCoordinateStartToGeneLocationDto>()
                    //opt => opt.Ignore()
                    )
                .ForMember(
                    geneLocationDto => geneLocationDto.End,
                    opt => opt.ResolveUsing<GeneCoordinateEndToGeneLocationDto>()
                    )
                ;
            CreateMap<GeneLocationDto, GeneLocation>()
                .ForMember(geneLocation => geneLocation.GeneCoordinates, opt => opt.Ignore())
                ;
        }
    }
}