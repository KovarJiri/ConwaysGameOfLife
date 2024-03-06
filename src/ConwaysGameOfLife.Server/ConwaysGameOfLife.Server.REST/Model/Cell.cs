namespace ConwaysGameOfLife.Server.Model
{
    /// <summary>
    /// Definition of age
    /// </summary>
    public class Cell
    {         
        // Coordination
        public int XCoord { get; init; } = 0;
        public int YCoord { get; init; } = 0;

        public Cell()
        {}

        public Cell(int xCoord, int yCoord)
        {
            XCoord = xCoord;
            YCoord = yCoord;
        }
    }
}
