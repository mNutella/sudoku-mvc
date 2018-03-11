using Microsoft.EntityFrameworkCore;
using Sudoku.Mvc.Data.Entity;

namespace Sudoku.Mvc.Data.Config
{
    public class SudokuDbContext : DbContext
    {
        public DbSet<AppConfig> AppConfig { get; set; }

        public DbSet<Board> Board { get; set; }

        public SudokuDbContext(DbContextOptions<SudokuDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
