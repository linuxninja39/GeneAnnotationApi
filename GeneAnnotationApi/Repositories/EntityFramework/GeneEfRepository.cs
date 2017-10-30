using GeneAnnotationApi.Entities;

namespace GeneAnnotationApi.Repositories.EntityFramework
{
    public class GeneEfRepository: BaseEfRepository<Gene>, IGeneRepository
    {
        public GeneEfRepository(GeneAnnotationDBContext context) : base(context)
        {
        }
    }
}