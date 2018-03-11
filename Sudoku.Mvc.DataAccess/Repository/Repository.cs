using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sudoku.Mvc.DataAccess.Entity;
using Sudoku.Mvc.DataAccess.RepositoryInterface;

namespace Sudoku.Mvc.DataAccess.Repository
{
    public class Repository<TContext> : ReadOnlyRepository<TContext>, IRepository, IDisposable where TContext : DbContext
    {
        private bool _disposed = false;

        public Repository(TContext context) : base(context)
        {
        }

        protected override IQueryable<TEntity> GetQueryable<TEntity>(
        Expression<Func<TEntity, bool>> filter = null,
        bool includeDeleted = false,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null)
        {
            return PrepareQueryable(filter, includeDeleted, orderBy, includeProperties, skip, take);
        }

        public virtual void Add<TEntity>(
            TEntity entity,
            string createdBy = null)
            where TEntity : class, IBaseEntity
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = createdBy;
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity, string deletedBy = null)
            where TEntity : class, IBaseEntity
        {
            entity.DeletedDate = DateTime.UtcNow;
            entity.DeletedBy = deletedBy;
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void DeleteById<TEntity>(object id, string deletedBy = null)
            where TEntity : class, IBaseEntity
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            Delete(entity, deletedBy);
        }

        /// <summary>
        /// We call only when it is necessary to send the transaction immediately
        /// </summary>
        /// <returns></returns>
        public virtual async Task Save()
        {
            var entities = from entry in new ChangeTracker(_context).Entries()
                           where entry.State == EntityState.Modified || entry.State == EntityState.Added
                           select entry.Entity;

            var validationResults = new List<ValidationResult>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                {
                    var fullErrorMessage = string.Join("; ", validationResults.Select(x => x.ErrorMessage));
                    throw new ValidationException(fullErrorMessage);
                }
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            Save().Wait();
            _disposed = true;
        }
    }
}