using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using ConwaysGameOfFile.DTO;
using ConwaysGameOfLife.Server.REST.Providers;
using ConwaysGameOfLife.Server.REST.Extensions;

namespace ConwaysGameOfLife.Server.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1/board")]
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;
        private readonly GameProvider _gameProvider;

        public BoardController(
            ILogger<BoardController> logger,
            GameProvider gameProvider)
        {
            _logger = logger;

            _gameProvider = gameProvider;
        }

        #region GET

        [HttpGet]
        [Route("{boardXSize:min(100):max(500)}/{boardYSize:min(100):max(500)}/{initVersion?}")]
        [SwaggerOperation("Returns init version of board.")]
        [ProducesResponseType<BoardDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Guid> GetNewBoard(int boardXSize, int boardYSize, int initVersion = 0)
        {
            try
            {
                var initBoardGuid = _gameProvider.GetNewBoard(boardXSize, boardYSize, initVersion);

                return Ok(initBoardGuid);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());

                return BadRequest("Sorry, problems with server. Please try later.");
            }
        }

        [HttpGet]
        [Route("{boardGuid}")]
        [SwaggerOperation("Returns actual version of board.")]
        [ProducesResponseType<BoardDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Guid> GetActualBoardState(string boardGuid)
        {
            try
            {
                Guid parsedGuid;

                if (!Guid.TryParse(boardGuid, out parsedGuid))
                    return BadRequest("Sorry, string does not represents GUID!");

                if (!_gameProvider.BoardExists(parsedGuid))
                    return BadRequest("Sorry, this board does not exists'");

                var board = _gameProvider.GetBoard(parsedGuid);
                
                var boardDto = board.ToDTO();

                return Ok(boardDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());

                return BadRequest("Sorry, problems with server. Please try later.");
            }
        }

        #endregion

        #region POST

        [HttpPost]
        [Route("")]
        [SwaggerOperation("Evolve board by one step.")]
        [ProducesResponseType<BoardDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BoardDto> EvolveBoard([FromBody]string boardGuid)
        {
            try
            {
                Guid parsedGuid;

                if (!Guid.TryParse(boardGuid, out parsedGuid))
                    return BadRequest("Sorry, string does not represents GUID!");

                if (!_gameProvider.BoardExists(parsedGuid))
                    return BadRequest("Sorry, this board does not exists'");

                var evolvedBoard = _gameProvider.EvolveBoard(parsedGuid);

                var evolvedBoardDto = evolvedBoard.ToDTO();

                return Ok(evolvedBoardDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());

                return BadRequest("Sorry, problems with server. Please try later.");
            }
        }

        #endregion
    }
}
