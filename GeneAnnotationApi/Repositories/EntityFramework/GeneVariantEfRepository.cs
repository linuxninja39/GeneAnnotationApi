using System.Linq;
using System.Reflection;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Repositories.EntityFramework
{
    public class GeneVariantEfRepository : BaseEfRepository<Gene>, IGeneVariantRepository
    {
        private IQueryable<GeneVariant> _geneVariants;

        public GeneVariantEfRepository(GeneAnnotationDBContext context) : base(context)
        {
            _geneVariants = _context.GeneVariant;
        }

        public IQueryable<GeneVariant> FindByRange(int start, int end)
        {
            return _geneVariants
                .Where(
                    gv => (gv.Start >= start && gv.End >= end && gv.End <= start)
                          || (gv.Start <= start && gv.End >= end)
                          || (gv.Start >= start && gv.End <= end)
                          || (gv.Start <= start && gv.End <= end && gv.End >= end)
                );
        }
    }
}