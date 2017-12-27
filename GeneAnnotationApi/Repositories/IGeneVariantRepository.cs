using System.Linq;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Repositories
{
    public interface IGeneVariantRepository: IBaseRepository<Gene>
    {
        IQueryable<GeneVariant> FindByRange(int start, int end);

    }
}