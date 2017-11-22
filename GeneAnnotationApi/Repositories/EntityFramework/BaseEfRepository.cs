using System.Linq;
using System.Reflection;
using GeneAnnotationApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneAnnotationApi.Repositories.EntityFramework
{
    public class BaseEfRepository<T>: IBaseRepository<T> where T : class
    {
        protected readonly GeneAnnotationDBContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseEfRepository(GeneAnnotationDBContext context)
        {
            _context = context;
            var typeName = typeof(T).Name;
            var propertyInfo = _context
                .GetType()
                .GetProperty(typeName);

            _dbSet = (DbSet<T>)propertyInfo.GetValue(_context, null);
        }
        
        public T Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> All()
        {
            return _dbSet;
        }

        public void Save(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}