using ConwaysGameOfFile.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Services
{
    public class BoardService
    {
        internal HttpClient httpClient;
        private readonly Common.API.BoardService.BoardApi _boardService;

        public BoardService() 
        {
            // TODO: Remove me - SSL Hack
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            httpClient = new HttpClient(handler);
            
            // DEBUG adress
            httpClient.BaseAddress = new Uri("https://localhost:10000");

            _boardService = new Common.API.BoardService.BoardApi(httpClient);
        }

        public async ValueTask<Guid> CreateNewBoard(int boardXSize, int boardYSize, int preset = 1)
        {
            return await _boardService.GetNewBoard(boardXSize, boardYSize, preset);
        }

        public async ValueTask<BoardDto> GetActualStateOfBoard(Guid guidOfBoard)
        {
            return await _boardService.GetActualBoard(guidOfBoard);
        }

        public async ValueTask<BoardDto> EvolveBoard(Guid guidOfBoard)
        {
            return await _boardService.EvolveBoard(guidOfBoard);
        }
    }
}
