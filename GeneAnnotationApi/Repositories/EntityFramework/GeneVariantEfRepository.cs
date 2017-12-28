using System.Linq;
using System.Reflection;
using GeneAnnotationApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneAnnotationApi.Repositories.EntityFramework
{
    public class GeneVariantEfRepository : BaseEfRepository<Gene>, IGeneVariantRepository
    {
        private readonly IQueryable<GeneVariant> _geneVariants;

        public GeneVariantEfRepository(GeneAnnotationDBContext context) : base(context)
        {
            _geneVariants = _context.GeneVariant;
        }

        public IQueryable<GeneVariant> FindByRange(int start, int end)
        {
            const string sql = @"
                SELECT *
                  FROM gene_variant
                  WHERE (start >= {0} AND [end] >= {1} AND [start] <= {1})
                        OR (start <= {0} AND [end] >= {1})
                        OR (start >= {0} AND [end] <= {1})
                        OR (start <= {0} AND [end] <= {1} AND [end] >= {0})";
            return _geneVariants.FromSql(sql, start, end);

            /*
            return _geneVariants
                .Where(
                    gv => (gv.Start >= start && gv.End >= end && gv.End <= start)
                          || (gv.Start <= start && gv.End >= end)
                          || (gv.Start >= start && gv.End <= end)
                          || (gv.Start <= start && gv.End <= end && gv.End >= end)
                );
                */
        }
    }
}