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
            try
            {
                return GetCoords(gene)
                        .Max(
                            geneCoordinate => geneCoordinate.End
                        )
                    ;
            }
            catch (InvalidOperationException e)
            {
                if (e.Message.Equals("Sequence contains no elements"))
                {
                    return null;
                }

                throw e;
            }
        }

        public int? FindMinByGene(Gene gene)
        {
            try
            {
                return GetCoords(gene)
                        .Min(
                            geneCoordinate => geneCoordinate.Start
                        )
                    ;
            }
            catch (InvalidOperationException e)
            {
                if (e.Message.Equals("Sequence contains no elements"))
                {
                    return null;
                }

                throw e;
            }
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
                );
        }
    }
}