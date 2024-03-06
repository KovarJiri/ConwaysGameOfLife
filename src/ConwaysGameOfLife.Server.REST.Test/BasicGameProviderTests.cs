using ConwaysGameOfLife.Server.REST.Test.Helpers;

namespace ConwaysGameOfLife.Server.REST.Test
{
    [TestClass]
    public class BasicGameProviderTests
    {
        [TestMethod]
        public void BoardWithOneCell_ShouldDieInNextStep()
        {
            var board = new int[,] {
                { 0,  0,  0,  0},
                { 0,  1,  0,  0},
                { 0,  0,  0,  0},
                { 0,  0,  0,  0}};

            var evolvedBoard = new int[,] {
                { 0,  0,  0,  0},
                { 0,  0,  0,  0},
                { 0,  0,  0,  0},
                { 0,  0,  0,  0}};

            Assert.That.BoolBoardIsEvolvedCorrectly(board, evolvedBoard);
        }

        [TestMethod]
        public void BoardWithSeparatedTwoCells_BothShouldDieInNextStep()
        {
            var board = new int[,] {
                { 0,  0,  0,  0},
                { 0,  1,  0,  0},
                { 0,  0,  0,  0},
                { 0,  0,  1,  0}};

            var evolvedBoard = new int[,] {
                { 0,  0,  0,  0},
                { 0,  0,  0,  0},
                { 0,  0,  0,  0},
                { 0,  0,  0,  0}};

            Assert.That.BoolBoardIsEvolvedCorrectly(board, evolvedBoard);
        }
    }
}