using System;
using System.Linq;
using GeneAnnotationApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneAnnotationApi.Repositories.EntityFramework
{
    public class GeneCoordinateEfRepository : BaseEfRepository<GeneCoordinate>, IGeneCoordinateRepository
    {
        public GeneCoordinateEfRepository(GeneAnnotationDBContext context) : base(context)
        {
        }

        public int? FindMaxByGene(Gene gene)
        {
            var coords = GetCoords(gene);
            if (!coords.Any())
            {
                return null;
            }

            return coords
                    .Max(
                        geneCoordinate => geneCoordinate.End
                    )
                ;
        }

        public int? FindMinByGene(Gene gene)
        {
            var coords = GetCoords(gene);
            if (!coords.Any())
            {
                return null;
            }

            return GetCoords(gene)
                    .Min(
                        geneCoordinate => geneCoordinate.Start
                    )
                ;
        }

        private IQueryable<GeneCoordinate> GetCoords(Gene gene)
        {
            return _dbSet
                    .Include(geneCoordinate => geneCoordinate.GeneLocation)
                    .ThenInclude(geneLocation => geneLocation.Gene)
                    .Where(
                        geneCoordinate => geneCoordinate.GeneLocation.HgVersion.Equals(19)
                    )
                    .Where(
                        geneCoordinate => geneCoordinate.GeneLocation.Gene.Equals(gene)
                    )
                ;
        }
    }
}