using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sudoku.Generator;
using Sudoku.Mvc.Bl.ServiceInterfaces;
using Sudoku.Mvc.Common.Configuration;
using Sudoku.Mvc.Data.Model.OutputDto.GameBoardService;
using Sudoku.Mvc.DataAccess.RepositoryInterface;
using Board = Sudoku.Mvc.Data.Entity.Board;

namespace Sudoku.Mvc.Bl.Service
{
    public class GameBoardService : IGameBoardService
    {
        private readonly ILogger _logger;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        private List<RowData> _filledBoard, _emptyBoard;
        private BoardGenerator _generator;

        public GameBoardService(IRepository repository, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<GameBoardService>();

            _filledBoard = _emptyBoard = new List<RowData>();
            _generator = new BoardGenerator();
        }

        #region Public

        public async Task<BoardDataOutput> CreateBoard()
        {
            _logger.LogDebug($"Service {nameof(GameBoardService)}, method {nameof(CreateBoard)}.");

            var board = new Board
            {
                FilledBoard = JsonConvert.SerializeObject(await GenerateFilledBoard()).Trim(),
                EmptyBoard = JsonConvert.SerializeObject(await GenerateEmptyBoard()).Trim()
            };
            _repository.Add(board);
            await _repository.Save();

            return _mapper.Map<Board, BoardDataOutput>(board);
        }

        public async Task<BoardDataOutput> GetBoard(int currentId)
        {
            _logger.LogDebug($"Service {nameof(GameBoardService)}, method {nameof(GetBoard)}.");

            var boards = await _repository.GetAll<Board>();
            var nextBoard = await GetNextBoard(currentId, boards);

            return nextBoard;
        }

        #endregion

        #region Private

        /// <summary>
        /// Get next game board from list all boards
        /// <param name="currentId">Id game board of user</param>
        /// <param name="boards">List all game boards from db</param>
        /// </summary>
        /// <returns>New game board</returns>
        private async Task<BoardDataOutput> GetNextBoard(int currentId, IEnumerable<Board> boards)
        {
            var counter = 0;

            foreach (var b in boards.OrderBy(t => t.Id))
            {
                if (b.Id == currentId)
                {
                    if (currentId + SudokuConstants.Step > boards.Count())
                    {
                        return _mapper.Map<Board, BoardDataOutput>(boards.First());
                    }

                    return _mapper.Map<Board, BoardDataOutput>(boards.ElementAt(counter + SudokuConstants.Step));
                }

                counter++;
            }

            return _mapper.Map<Board, BoardDataOutput>(boards.First());
        }

        /// <summary>
        /// Generate a new filled board
        /// </summary>
        /// <returns>List with a filled board</returns>
        private async Task<List<RowData>> GenerateFilledBoard()
        {
            _generator.generateSolutionBoard();
            String[] stringMatrixBoard = _generator.getSolutionBoard().toString(true).Split('\n');
            for (Int32 i = 0; i < stringMatrixBoard.Length - 1; i++)
            {
                RowData rowData = new RowData() { Row = new List<CellData>() };
                for (Int32 j = 0; j < stringMatrixBoard[i].Length; j++)
                {
                    CellData valueData = new CellData()
                    {
                        Value = Int32.Parse(stringMatrixBoard[i][j].ToString()),
                        ReadOnly = true
                    };
                    rowData.Row.Add(valueData);
                }
                _filledBoard.Add(rowData);
            }

            return _filledBoard;
        }

        /// <summary>
        /// Generate a new empty board
        /// </summary>
        /// <returns>List with an empty board</returns>
        private async Task<List<RowData>> GenerateEmptyBoard()
        {
            for (Int32 i = 0; i < _filledBoard.Count; i++)
            {
                RowData rowData = _filledBoard[i];
                for (Int32 j = 0; j < rowData.Row.Count; j++)
                {
                    CellData valueData = rowData.Row.ElementAt(j);
                    if ((i % 2 != 0) && (j % 2 != 0))
                    {
                        valueData.Value = null;
                        valueData.ReadOnly = false;
                    }
                }
            }

            return _emptyBoard;
        }

        #endregion
    }
}