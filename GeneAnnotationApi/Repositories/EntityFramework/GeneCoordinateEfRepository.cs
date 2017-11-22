using System.Linq;
using GeneAnnotationApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneAnnotationApi.Repositories.EntityFramework
{
    public class GeneCoordinateEfRepository: BaseEfRepository<GeneCoordinate>, IGeneCoordinateRepository
    {
        public GeneCoordinateEfRepository(GeneAnnotationDBContext context) : base(context)
        {
        }

        public int FindMaxByGene(Gene gene)
        {
            var max = _dbSet
                .Include(geneCoordinate => geneCoordinate.GeneLocation)
                .ThenInclude(geneLocation => geneLocation.Gene)
                .Where(
                    geneCoordinate => geneCoordinate.GeneLocation.HgVersion.Equals(19)
                    )
                .Where(
                    geneCoordinate => geneCoordinate.GeneLocation.Gene.Equals(gene)
                    )
                .Max(geneCoordinate => geneCoordinate.Start)
                ;
            return 2;
        }

        public int FindMinByGene(Gene gene)
        {
            return 1;
        }
    }
}