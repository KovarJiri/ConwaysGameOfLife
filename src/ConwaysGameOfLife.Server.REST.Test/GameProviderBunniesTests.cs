using ConwaysGameOfLife.Server.REST.Test.Helpers;

namespace ConwaysGameOfLife.Server.REST.Test
{
    [TestClass]
    public class GameProviderBunniesTests
    {
        [TestMethod]
        public void BoardBunnies_ShouldEvolveCorrectly()
        {
            var board = new int[,] { 
                { 1,  0,  0,  0,  0,  0,  1,  0,  0,  0},
                { 0,  0,  1,  0,  0,  0,  1,  0,  0,  0},
                { 0,  0,  1,  0,  0,  1,  0,  1,  0,  0},
                { 0,  1,  0,  1,  0,  0,  0,  0,  0,  0},
                { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0}};

            var evolvedBoardStep1 = new int[,] {
                { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                { 0,  1,  0,  0,  0,  1,  1,  1,  0,  0},
                { 0,  1,  1,  1,  0,  0,  1,  0,  0,  0},
                { 0,  0,  1,  0,  0,  0,  0,  0,  0,  0},
                { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0}};

            var evolvedBoardStep2 = new int[,] {
                { 0,  0,  0,  0,  0,  0,  1,  0,  0,  0},
                { 0,  1,  0,  0,  0,  1,  1,  1,  0,  0},
                { 0,  1,  0,  1,  0,  1,  1,  1,  0,  0},
                { 0,  1,  1,  1,  0,  0,  0,  0,  0,  0},
                { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0},
                { 0,  0,  0,  0,  0,  0,  0,  0,  0,  0}};

            Assert.That.BoolBoardIsEvolvedCorrectly(board, evolvedBoardStep1);
            Assert.That.BoolBoardIsEvolvedCorrectly(evolvedBoardStep1, evolvedBoardStep2);
        }
    }
}