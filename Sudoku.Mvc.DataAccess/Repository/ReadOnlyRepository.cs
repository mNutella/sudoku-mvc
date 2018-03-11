using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sudoku.Mvc.DataAccess.Entity;
using Sudoku.Mvc.DataAccess.RepositoryInterface;

namespace Sudoku.Mvc.DataAccess.Repository
{
    public class ReadOnlyRepository<TContext> : IReadOnlyRepository where TContext : DbContext
    {
        protected readonly TContext _context;

        public ReadOnlyRepository(TContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<TEntity> PrepareQueryable<TEntity>(
                Expression<Func<TEntity, bool>> filter = null,
                bool includeDeleted = false,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                string includeProperties = null,
                int? skip = null,
                int? take = null)
                where TEntity : class, IBaseEntity
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeDeleted ? query.Where(x => x.DeletedDate.HasValue) 
                                   : query.Where(x => !x.DeletedDate.HasValue);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
                Expression<Func<TEntity, bool>> filter = null,
                bool includeDeleted = false,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                string includeProperties = null,
                int? skip = null,
                int? take = null)
                where TEntity : class, IBaseEntity
        {
            return PrepareQueryable(filter, includeDeleted, orderBy, includeProperties, skip, take).AsNoTracking();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll<TEntity>(
           bool includeDeleted = false,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = null,
           int? skip = null,
           int? take = null)
           where TEntity : class, IBaseEntity
        {
            return await GetQueryable(null, includeDeleted, orderBy, includeProperties, skip, take).ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> Get<TEntity>(
          Expression<Func<TEntity, bool>> filter = null,
          bool includeDeleted = false,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = null,
          int? skip = null,
          int? take = null)
          where TEntity : class, IBaseEntity
        {
            return await GetQueryable(filter, includeDeleted, orderBy, includeProperties, skip, take).ToListAsync().ConfigureAwait(false);
        }

        public virtual Task<TEntity> GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            bool includeDeleted = false,
            string includeProperties = null)
            where TEntity : class, IBaseEntity
        {
            return GetQueryable(filter, includeDeleted, null, includeProperties).SingleOrDefaultAsync();
        }

        public virtual Task<TEntity> GetFirst<TEntity>(
           Expression<Func<TEntity, bool>> filter = null,
           bool includeDeleted = false,
           Func<IQueryable<TEntity>, 
           IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = null)
           where TEntity : class, IBaseEntity
        {
            return GetQueryable(filter, includeDeleted, orderBy, includeProperties).FirstOrDefaultAsync();
        }

        public virtual Task<TEntity> GetById<TEntity>(object id)
             where TEntity : class, IBaseEntity
        {
            return _context.Set<TEntity>().FindAsync(id);
        }

        public virtual Task<int> GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null,
            bool includeDeleted = false)
                where TEntity : class, IBaseEntity
        {
            return GetQueryable(filter, includeDeleted).CountAsync();
        }
    }
}
