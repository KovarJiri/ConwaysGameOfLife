using ConwaysGameOfLife.Server.REST.Test.Helpers;

namespace ConwaysGameOfLife.Server.REST.Test
{
    [TestClass]
    public class GameProviderTests
    {
        [TestMethod]
        public void BoardWithTetriom_ShouldDieInTwoEvolveSteps()
        {
            var board = new int[,] { 
                { 0,  0,  0,  0},
                { 0,  0,  0,  1},
                { 0,  0,  1,  0},
                { 0,  1,  0,  0}};

            var evolvedBoardStep1 = new int[,] {
                { 0,  0,  0,  0},
                { 0,  0,  0,  0},
                { 0,  0,  1,  0},
                { 0,  0,  0,  0}};

            var evolvedBoardStep2 = new int[,] {
                { 0,  0,  0,  0},
                { 0,  0,  0,  0},
                { 0,  0,  0,  0},
                { 0,  0,  0,  0}};

            Assert.That.BoolBoardIsEvolvedCorrectly(board, evolvedBoardStep1);
            Assert.That.BoolBoardIsEvolvedCorrectly(evolvedBoardStep1, evolvedBoardStep2);
        }

        [TestMethod]
        public void BoardWithStableSquare_ShouldEndureThruEntireSimulation()
        {
            var board = new int[,] { 
                { 0,  0,  0,  0},
                { 0,  1,  1,  0},
                { 0,  1,  1,  0},
                { 0,  0,  0,  0}};

            var evolvedBoard = new int[,] {
                { 0,  0,  0,  0},
                { 0,  1,  1,  0},
                { 0,  1,  1,  0},
                { 0,  0,  0,  0}};

            Assert.That.BoolBoardIsEvolvedCorrectly(board, evolvedBoard);
        }

        [TestMethod]
        public void BoardWithThreeCells_EndsUpWithSquarePosition()
        {
            var board = new int[,] { 
                { 0,  0,  0,  0},
                { 0,  1,  1,  0},
                { 0,  1,  0,  0},
                { 0,  0,  0,  0}};

            var evolvedBoard = new int[,] { 
                { 0,  0,  0,  0},
                { 0,  1,  1,  0},
                { 0,  1,  1,  0},
                { 0,  0,  0,  0}};

            Assert.That.BoolBoardIsEvolvedCorrectly(board, evolvedBoard);
        }
    }
}