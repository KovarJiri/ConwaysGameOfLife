using ConwaysGameOfFile.DTO;
using ConwaysGameOfLife.Server.Model;

namespace ConwaysGameOfLife.Server.REST.Extensions
{
    public static class BoardDtoExtensions
    {
        // Board converters
        public static BoardDto ToDTO(this Model.Board board) =>
            new BoardDto()
            {
                Cells = board.GetCells.ToDTO(),
                BoardSizeX = board.BoardSizeX,
                BoardSizeY = board.BoardSizeY
            };

        public static Model.Board FromDTO(this BoardDto boardDto) =>
            new Board(boardDto.BoardSizeX, boardDto.BoardSizeY)
                .ReplaceCells(boardDto.Cells.FromDTO());
    }
}
