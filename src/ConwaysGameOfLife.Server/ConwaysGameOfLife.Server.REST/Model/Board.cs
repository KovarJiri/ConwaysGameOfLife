using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace ConwaysGameOfLife.Server.Model
{
    /// <summary>
    /// Definition of board
    /// </summary>
    public class Board
    {
        public int BoardSizeX { get; init; } = 500;
        public int BoardSizeY { get; init; } = 500;

        private List<Cell> _boardCells;
        public List<Cell> GetCells => _boardCells;

        public Board(int boardSizeX = 500, int boardSizeY = 500)
        {
            BoardSizeX = boardSizeX < 1 ? 500 : boardSizeX;
            BoardSizeY = boardSizeY < 1 ? 500 : boardSizeY;

            _boardCells = new List<Cell>();
        }

        // Replaced all cells
        public Board ReplaceCells(IEnumerable<Cell> cells)
        {
            if (!cells.Any())
                throw new NullReferenceException("Board cells are not defined!");

            _boardCells = cells.ToList();

            return this;
        }

        // Add one cell
        internal void AddCell(int xCoord, int yCoord)
        {
            _boardCells.Add(new Cell(xCoord, yCoord));
        }

        // Delete all cells
        internal void CleanCells()
        {
            _boardCells.Clear();
        }
    }
}
