namespace Sudoku.Mvc.Data.Model.OutputDto.GameBoardService
{
    public class BoardDataOutput
    {
        /// <summary>
        /// Id boards
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Json filled data
        /// </summary>
        public string FilledBoard { get; set; }

        /// <summary>
        /// Json empty data
        /// </summary>
        public string EmptyBoard { get; set; }
    }
}
