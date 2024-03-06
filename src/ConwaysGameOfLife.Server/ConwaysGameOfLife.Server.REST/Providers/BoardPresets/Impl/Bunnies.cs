using ConwaysGameOfLife.Server.Model;

namespace ConwaysGameOfLife.Server.REST.Providers.BoardPresets
{
    public class Bunnies : IBoardPresets
    {
        public static Board Init(int boardXSize, int boardYSize)
        {
            int middleOfBoardX = boardXSize / 2;
            int middleOfBoardY = boardYSize / 2;

            List<Cell> bunnies = new List<Cell>();
            
            bunnies.Add(new Cell(middleOfBoardX + 0, middleOfBoardY + 0));
            bunnies.Add(new Cell(middleOfBoardX + 6, middleOfBoardY + 0));
            bunnies.Add(new Cell(middleOfBoardX + 2, middleOfBoardY + 1));
            bunnies.Add(new Cell(middleOfBoardX + 6, middleOfBoardY + 1));
            bunnies.Add(new Cell(middleOfBoardX + 2, middleOfBoardY + 2));
            bunnies.Add(new Cell(middleOfBoardX + 5, middleOfBoardY + 2));
            bunnies.Add(new Cell(middleOfBoardX + 7, middleOfBoardY + 2));
            bunnies.Add(new Cell(middleOfBoardX + 1, middleOfBoardY + 3));
            bunnies.Add(new Cell(middleOfBoardX + 3, middleOfBoardY + 3));

            return new Board(boardXSize, boardYSize).ReplaceCells(bunnies);
        }
    }
}
