using System.ComponentModel.DataAnnotations;
using Sudoku.Mvc.Data.Entity.Abstract;

namespace Sudoku.Mvc.Data.Entity
{
    public class Board : BaseEntity<int>
    {
        [Required]
        public string FilledBoard { get; set; }

        [Required]
        public string EmptyBoard { get; set; }
    }
}
