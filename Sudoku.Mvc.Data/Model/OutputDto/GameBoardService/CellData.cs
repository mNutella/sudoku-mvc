using System;

namespace Sudoku.Mvc.Data.Model.OutputDto.GameBoardService
{
    public class CellData
    {
        /// <summary>
        /// Cell value
        /// </summary>
        public Int32? Value { get; set; }
        
        /// <summary>
        /// Cell parametr
        /// </summary>
        public Boolean ReadOnly { get; set; }
    }
}
