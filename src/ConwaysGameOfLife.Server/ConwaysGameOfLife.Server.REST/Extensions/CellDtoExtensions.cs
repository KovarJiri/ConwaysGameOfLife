using ConwaysGameOfFile.DTO;
using ConwaysGameOfLife.Server.Model;

namespace ConwaysGameOfLife.Server.REST.Extensions
{
    public static class CellDtoExtensions
    {
        // Basic converters
        public static CellDto ToDTO(this Model.Cell cell) => new CellDto() { 
            XCoord = cell.XCoord,
            YCoord = cell.YCoord
        };

        public static Model.Cell FromDTO(this CellDto cellDto) => new Model.Cell()
        {
            XCoord = cellDto.XCoord,
            YCoord = cellDto.YCoord,
        };

        // IEnumerable converters
        public static IEnumerable<Model.Cell> FromDTO(this IEnumerable<CellDto> cellDtos)
        {
            if (cellDtos == null)
                yield break;

            foreach (CellDto cellDto in cellDtos)
            {
                yield return cellDto.FromDTO();
            }
        }

        public static IEnumerable<CellDto> ToDTO(this IEnumerable<Cell> cells)
        {
            if (cells == null)
                yield break;

            foreach (Cell cell in cells)
            {
                yield return cell.ToDTO();
            }
        }
    }
}
