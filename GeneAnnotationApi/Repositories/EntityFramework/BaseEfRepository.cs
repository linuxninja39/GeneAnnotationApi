using System.Linq;
using System.Reflection;
using GeneAnnotationApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeneAnnotationApi.Repositories.EntityFramework
{
    public class BaseEfRepository<T>: IBaseRepository<T> where T : class
    {
        private GeneAnnotationDBContext _context;
        private DbSet<T> _dbSet;

        public BaseEfRepository(GeneAnnotationDBContext context)
        {
            _context = context;
            var typeName = typeof(T).Name;
            var propertyInfo = _context
                .GetType()
                .GetProperty(typeName);

            var dbSet = propertyInfo.GetValue(_context, null);
        }
        
        public T Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> All()
        {
            throw new System.NotImplementedException();
        }

        public void Save(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}