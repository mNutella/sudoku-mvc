using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sudoku.Mvc.Bl.ServiceInterfaces;
using Sudoku.Mvc.Web.Common;

namespace Sudoku.Mvc.Api.Controllers.Rest.v1
{
    /// <summary>
    /// Controller for interacton with game board
    /// </summary>
    [Route("api/v1/[controller]")]
    public class GameBoardController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IGameBoardService _gameBoardService;

        public GameBoardController(IGameBoardService gameBoardService, IMapper mapper)
        {
            _mapper = mapper;
            _gameBoardService = gameBoardService;
        }

        /// <summary>
        /// Create new game board
        /// </summary>
        /// <returns>Json data with filled and empty game board</returns>
        [HttpPost("[action]")]
        public async Task<object> CreateBoard()
        {
            var gameBoardModel = await _gameBoardService.CreateBoard();

            return new
            {
                id = gameBoardModel.Id,
                filledBoard = gameBoardModel.FilledBoard,
                emptyBoard = gameBoardModel.EmptyBoard
            };
        }

        /// <summary>
        /// Get next game board from list all boards
        /// <param name="id">Id game board of user</param>
        /// </summary>
        /// <returns>Json data with new board for user</returns>
        [HttpGet("{id}", Name = "GetBoard")]
        public async Task<object> GetBoard(int id)
        {
            var gameBoardModel = await _gameBoardService.GetBoard(id);

            return new
            {
                id = gameBoardModel.Id,
                filledBoard = gameBoardModel.FilledBoard,
                emptyBoard = gameBoardModel.EmptyBoard
            };

        }
    }
}
