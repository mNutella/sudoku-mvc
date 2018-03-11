using System.Threading.Tasks;
using Sudoku.Mvc.Data.Model.OutputDto.GameBoardService;

namespace Sudoku.Mvc.Bl.ServiceInterfaces
{
    /// <summary>
    /// Service for work with game data
    /// </summary>
    public interface IGameBoardService
    {
        /// <summary>
        /// Generate a new board
        /// </summary>
        /// <returns>Json string with game board</returns>
        Task<BoardDataOutput> CreateBoard();

        /// <summary>
        /// Returns an existing game board didn't play
        /// </summary>
        /// <param name="currentId">Id of game board in which played user</param>
        /// <returns>Json string with game board</returns>
        Task<BoardDataOutput> GetBoard(int currentId);
    }
}