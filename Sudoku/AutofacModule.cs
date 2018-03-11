using Autofac;
using Sudoku.Mvc.Bl.Service;
using Sudoku.Mvc.Bl.ServiceInterfaces;
using Sudoku.Mvc.Data.Config;
using Sudoku.Mvc.DataAccess.Repository;
using Sudoku.Mvc.DataAccess.RepositoryInterface;

namespace Sudoku.Mvc.Api
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //services
            builder.RegisterType<GameBoardService>()
                .As<IGameBoardService>()
                .InstancePerLifetimeScope();

            //repository
            builder.RegisterType<Repository<SudokuDbContext>>()
                .As<IRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ReadOnlyRepository<SudokuDbContext>>()
                .As<IReadOnlyRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
