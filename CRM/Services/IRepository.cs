using System.Linq.Expressions;

namespace CRM.Services
{
    public interface IRepository
    {
        public IQueryable<T> Get<T>(
            Expression<Func<T, bool>> Filter = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> OrderBy = null,
            bool AsNoTracking = false,
            params string[] IncludeProperties) where T : class;
        public void Add<T>(T entity) where T : class;
        public void Update<T>(T entity) where T : class;
        public void Delete<T>(T entity) where T : class;
        public void Save();
    }
}
