using ConwaysGameOfFile.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.WPF.Models
{
    public class GraphicCell
    {
        public double CanvasXCoord { get; init; }
        public double CanvasYCoord { get; init; }
        public double Width { get; init; }
        public double Height { get; init; }
    }
}
