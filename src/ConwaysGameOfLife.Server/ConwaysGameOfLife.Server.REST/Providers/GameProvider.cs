using ConwaysGameOfLife.Server.Model;
using ConwaysGameOfLife.Server.REST.Providers.BoardPresets;

namespace ConwaysGameOfLife.Server.REST.Providers
{
    public sealed class GameProvider
    {
        // Consts
        private const int CellIsDead = 0;
        private const int CellIsAlive = 1;

        // List of boards
        private readonly List<KeyValuePair<Guid, Board>> _boards;

        public GameProvider()
        {
            _boards = new();
        }

        /// <summary>
        /// Basic evolve funtion
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public Board EvolveBoard(Guid boardGuid)
        {
            Board boardToEvolve = GetBoard(boardGuid);

            int[,] board = BoardToArray(boardToEvolve);

            var evolvedMinimalBoard = Evolve(board, boardToEvolve.BoardSizeX, boardToEvolve.BoardSizeY);

            boardToEvolve.CleanCells();

            for (var i = 0; i < evolvedMinimalBoard.GetLength(0); i++)
            {
                for (var j = 0; j < evolvedMinimalBoard.GetLength(1); j++)
                { 
                    if (evolvedMinimalBoard[i,j] > 0) 
                    {
                        boardToEvolve.AddCell(i, j);
                    }
                }
            }

            return boardToEvolve;
        }

        private int[,] BoardToArray(Board boardToEvolve)
        {
            var minimalBoard = new int[boardToEvolve.BoardSizeX, boardToEvolve.BoardSizeY];

            foreach (var cell in boardToEvolve.GetCells)
            {
                minimalBoard[cell.XCoord, cell.YCoord] = 1;
            }

            return minimalBoard;
        }

        // Returns new board and register id of it
        public Guid GetNewBoard(int boardXSize, int boardYSize, int preset)
        {
            var newBoardGuid = Guid.NewGuid();

            var board = GetPresetBoard(boardXSize, boardYSize, preset);

            _boards.Add(new KeyValuePair<Guid, Board>(newBoardGuid, board));

            return newBoardGuid;
        }

        #region Board presets
        public static Board GetPresetBoard(int boardXSize, int boardYSize, int preset) => preset switch
        {
            1 => Bunnies.Init(boardXSize, boardYSize),
            _ => new Board(boardXSize, boardYSize)
        };
        #endregion

        #region Implementation of game of file logic

        /// <summary>
        /// Basic functionaly for evolving board by 1 step
        /// </summary>
        /// <param name="intBoard"></param>
        /// <param name="Rows"></param>
        /// <param name="Columns"></param>
        /// <returns></returns>
        public static int[,] Evolve(int[,] intBoard, int Rows, int Columns)
        {
            int[,] newState = new int[Rows, Columns];

            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    newState[i, j] = NextStateForCell(intBoard, i, j, Rows, Columns);
                }
            }

            return newState;
        }

        /// <summary>
        /// Get state for actual cell in next step
        /// </summary>
        /// <param name="board"></param>
        /// <param name="actualXCoord"></param>
        /// <param name="actualYCoord"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private static int NextStateForCell(int[,] board, int actualXCoord, int actualYCoord, int rows, int columns)
        {
            int numOfSurrCellsAlive = CalculateLivingNeighbors(board, actualXCoord, actualYCoord, rows, columns);

            int cellValue = board[actualXCoord, actualYCoord];

            switch (cellValue)
            {
                case CellIsDead when numOfSurrCellsAlive == 3:
                    return CellIsAlive;
                case CellIsAlive when (numOfSurrCellsAlive < 2 || numOfSurrCellsAlive > 3):
                    return CellIsDead;
                default:
                    return cellValue;
            }
        }

        /// <summary>
        /// Get number of living sorrounding cells
        /// </summary>
        /// <param name="board"></param>
        /// <param name="actualXCoord"></param>
        /// <param name="actualYCoord"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private static int CalculateLivingNeighbors(int[,] board, int actualXCoord, int actualYCoord, int rows, int columns)
        {
            int neighCellsAlive = 0;

            // Go thru sorrounded cells
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (actualXCoord + x < 0 || actualXCoord + x > (rows - 1) || y + actualYCoord < 0 || y + actualYCoord > (columns - 1))
                    {
                        continue;
                    }
                    neighCellsAlive += board[actualXCoord + x, y + actualYCoord];
                }
            }

            neighCellsAlive -= board[actualXCoord, actualYCoord];
            
            return neighCellsAlive;
        }

        #endregion

        #region Board list helpers

        internal bool BoardExists(Guid parsedGuid) => _boards.Exists(kvp => kvp.Key == parsedGuid);

        internal Board GetBoard(Guid parsedGuid) => _boards.Find(kvp => kvp.Key == parsedGuid).Value;

        #endregion
    }
}
