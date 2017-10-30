using System.Linq;
using Microsoft.EntityFrameworkCore.Design;

namespace GeneAnnotationApi.Repositories
{
    public interface IBaseRepository<T>
    {
        T Get(int id);
        IQueryable<T> All();
        void Save(T entity);
    }
}