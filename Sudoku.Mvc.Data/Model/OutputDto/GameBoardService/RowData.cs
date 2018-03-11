using System.Collections.Generic;

namespace Sudoku.Mvc.Data.Model.OutputDto.GameBoardService
{
    public class RowData
    {
        /// <summary>
        /// Row data
        /// </summary>
        public ICollection<CellData> Row;

        public RowData()
        {
            Row = new List<CellData>();
        }
    }
}
