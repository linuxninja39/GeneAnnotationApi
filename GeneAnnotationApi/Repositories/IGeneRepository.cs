using System.Linq;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Repositories
{
    public interface IGeneRepository: IBaseRepository<Gene>
    {
        IQueryable<Gene> FindByStartAndEnd(int start, int end);

    }
}