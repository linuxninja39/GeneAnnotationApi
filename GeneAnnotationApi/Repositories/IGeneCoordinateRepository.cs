using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Repositories
{
    public interface IGeneCoordinateRepository
    {
        int? FindMaxByGene(Gene gene);
        int? FindMinByGene(Gene gene);
    }
}