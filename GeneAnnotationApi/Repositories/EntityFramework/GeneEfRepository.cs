using System.Linq;
using System.Reflection;
using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Repositories.EntityFramework
{
    public class GeneEfRepository: BaseEfRepository<Gene>, IGeneRepository
    {
        private static readonly int assemblyVersion = 19;
        public GeneEfRepository(GeneAnnotationDBContext context) : base(context)
        {
        }

        public IQueryable<Gene> FindByStartAndEnd(int start, int end)
        {
            throw new System.NotImplementedException();
        }
    }
}