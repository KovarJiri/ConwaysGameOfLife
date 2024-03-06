using ConwaysGameOfFile.DTO;
using ConwaysGameOfLife.Server.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics;
using System.Net;

namespace ConwaysGameOfLife.REST.APITest
{
    [TestClass]
    public class APITest
    {
        // Test with DTOs JSON serialization
        public Common.API.BoardService.BoardApi _boardService = null!;

        public WebApplicationFactory<ConwaysGameOfLife.Server.Program>? _application;

        [TestInitialize]
        public void TestInitialize()
        {
            _application = new WebApplicationFactory<ConwaysGameOfLife.Server.Program>();

            var _httpClient = _application.CreateClient();

            _boardService = new Common.API.BoardService.BoardApi(_httpClient);
        }

        [TestMethod]
        public async Task GetBoardMethod_ShoudlReturnOK_ResponseOfTypeBoardDto()
        {
            var response = await _boardService.GetNewBoard();

            Assert.IsInstanceOfType<Guid>(response);
        }

        [TestMethod]
        public async Task GetBoardMethod_ShoudlReturnGuid()
        {
            var response = await _boardService.GetNewBoard(200,200,1);

            Assert.IsInstanceOfType<Guid>(response);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task GetBoardMethod_WithBothInvalidParamsUnderLimit_ShoudlThrowException()
        {
            await _boardService.GetNewBoard(50, 50, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task GetBoardMethod_WithBothInvalidParamsOverLimit_ShoudlThrowException()
        {
            await _boardService.GetNewBoard(501, 501, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task GetBoardMethod_WithOneInvalidParamsOverLimit_ShoudlThrowException()
        {
            await _boardService.GetNewBoard(410, 501, 1);
        }

        [TestMethod]
        public async Task GetBoardMethod_ShoudlReturnBoardDto_WithBunniesOnBoard()
        {
            var response = await _boardService.GetNewBoard(200, 200);

            Assert.IsInstanceOfType<Guid>(response);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task EvolveBoardMethod_RequestWithNonValidGuid_ShouldThrowException()
        {
            await _boardService.EvolveBoard(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task EvolveBoardMethod_RequestWithEmptyGuid_ShouldThrowException()
        {
            await _boardService.EvolveBoard(Guid.Empty);
        }

        [TestMethod]
        public async Task GetActualBoardMethod_ShoudlReturnBoardDto_WithBunniesInInitialStateInMiddleOfBoard()
        {
            var response = await _boardService.GetNewBoard(300, 300, 1);

            var actualStateOfBoard = await _boardService.GetActualBoard(response);

            Assert.IsTrue(actualStateOfBoard.Cells.Count() == 9);
        }

        [TestMethod]
        public async Task EvolveBoardMethod_ShoudlReturnBoardDto_WithBunniesOnBoardAndEvolvedTwoSteps()
        {
            var response = await _boardService.GetNewBoard(500, 500,1);

            var evolvedBunniesbyOneStep = await _boardService.EvolveBoard(response);

            Assert.IsTrue(evolvedBunniesbyOneStep.Cells.Count() == 9);

            var evolvedBunniesbyTwoStep = await _boardService.EvolveBoard(response);

            Assert.IsTrue(evolvedBunniesbyTwoStep.Cells.Count() == 13);
        }
    }
}