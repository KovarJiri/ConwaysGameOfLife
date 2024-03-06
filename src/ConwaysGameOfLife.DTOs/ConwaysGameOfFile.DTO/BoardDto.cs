using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConwaysGameOfFile.DTO
{
    public readonly struct BoardDto
    {
        [JsonInclude]
        [Required(ErrorMessage = "BoardSizeX is required!")]
        [Range(100, 500, ErrorMessage = "Only positive number allowed between 100 and 500 allowed.")]
        public int BoardSizeX { get; init; }

        [JsonInclude]
        [Required(ErrorMessage = "BoardSizeY is required!")]
        [Range(100, 500, ErrorMessage = "Only positive number allowed between 100 and 500 allowed.")]
        public int BoardSizeY { get; init; }

        [JsonInclude]
        [Required(ErrorMessage = "Board array is required!")]
        public IEnumerable<CellDto> Cells { get; init; }
    }
}
