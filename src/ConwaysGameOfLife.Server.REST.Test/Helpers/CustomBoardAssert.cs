using ConwaysGameOfLife.Server.REST.Providers;

namespace ConwaysGameOfLife.Server.REST.Test.Helpers
{
    internal static class CustomBoardAssert
    {
        public static void BoolBoardIsEvolvedCorrectly(this Assert assert, int[,] initialBoard, int[,] correctlyEvolvedBoard)
        {
            var evolvedBoardByEngine = GameProvider.Evolve(initialBoard, initialBoard.GetLength(0), initialBoard.GetLength(1));

            for (int i = 0; i < initialBoard.GetLength(0); i++)
            {
                for (int j = 0; j < initialBoard.GetLength(1); j++)
                {
                    Assert.AreEqual(evolvedBoardByEngine[i, j], correctlyEvolvedBoard[i, j]);
                }
            }
        }
    }
}