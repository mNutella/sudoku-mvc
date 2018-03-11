using Sudoku.Mvc.Data.Entity.Abstract;

namespace Sudoku.Mvc.Data.Entity
{
    public class AppConfig : BaseEntity<int>
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
