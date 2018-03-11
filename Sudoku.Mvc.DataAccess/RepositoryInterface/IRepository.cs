using System.Threading.Tasks;
using Sudoku.Mvc.DataAccess.Entity;

namespace Sudoku.Mvc.DataAccess.RepositoryInterface
{
    public interface IRepository : IReadOnlyRepository
    {
        void Add<TEntity>(
            TEntity entity,
            string createdBy = null)
            where TEntity : class, IBaseEntity;

        void Delete<TEntity>(
            TEntity entity,
            string deletedBy = null)
            where TEntity : class, IBaseEntity;

        void DeleteById<TEntity>(
            object id,
            string deletedBy = null)
            where TEntity : class, IBaseEntity;

        Task Save();
    }
}
