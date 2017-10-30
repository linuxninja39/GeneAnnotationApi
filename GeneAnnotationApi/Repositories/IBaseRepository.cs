using Microsoft.EntityFrameworkCore.Design;

namespace GeneAnnotationApi.Repositories
{
    public interface IBaseRepository<T>
    {
        T Get(int id);
        void Save(T entity);
    }
}