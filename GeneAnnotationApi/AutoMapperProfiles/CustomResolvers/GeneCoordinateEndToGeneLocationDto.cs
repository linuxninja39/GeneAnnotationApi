using AutoMapper;
using GeneAnnotationApi.Dtos;
using GeneAnnotationApi.Entities;
using GeneAnnotationApi.Repositories;

namespace GeneAnnotationApi.AutoMapperProfiles.CustomResolvers
{
    public class GeneCoordinateEndToGeneLocationDto:
        IValueResolver<GeneLocation, GeneLocationDto, int?>
    {
        private readonly IGeneCoordinateRepository _geneCoordinateRepository;

        public GeneCoordinateEndToGeneLocationDto(
            IGeneCoordinateRepository geneCoordinateRepository
            )
        {
            _geneCoordinateRepository = geneCoordinateRepository;
        }

        public int? Resolve(GeneLocation source, GeneLocationDto destination, int? destMember,
            ResolutionContext context)
        {
            return _geneCoordinateRepository.FindMaxByGene(source.Gene);
        }
    }
}