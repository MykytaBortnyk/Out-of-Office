using Microsoft.EntityFrameworkCore;
using Serilog.Context;
using System.Linq;
using System.Linq.Expressions;
using CRM.Models;
using CRM.Data;

namespace CRM.Services
{
    public class Repository : IRepository, IDisposable
    {
        private readonly CrmContext _context;

        public Repository(IDbContextFactory<CrmContext> dbContextFactory)
            => _context = dbContextFactory.CreateDbContext();

        public IQueryable<T> Get<T>(
            Expression<Func<T, bool>> Filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null,
            bool AsNoTracking = false,
            params string[] IncludeProperties) where T : class
        {
            IQueryable<T> query = _context.Set<T>();

            if (Filter != null)
            {
                query = query.Where(Filter);
            }

            foreach (var includeProperty in IncludeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (AsNoTracking)
                query.AsNoTracking();

            return OrderBy is null ? query : OrderBy(query);
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}