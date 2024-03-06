using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConwaysGameOfFile.DTO
{
    public readonly struct CellDto
    {
        [JsonInclude]
        [Required(ErrorMessage = "XCoord is required!")]
        [Range(100, 500, ErrorMessage = "Only positive number allowed between 100 and 500 allowed.")]
        public int XCoord { get; init; }

        [JsonInclude]
        [Required(ErrorMessage = "YCoord is required!")]
        [Range(100, 500, ErrorMessage = "Only positive number allowed between 100 and 500 allowed.")]
        public int YCoord { get; init; }

        [JsonInclude]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public int Age { get; init; }
    }
}
