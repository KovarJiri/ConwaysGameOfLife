using ConwaysGameOfLife.Server.Model;

namespace ConwaysGameOfLife.Server.REST.Providers.BoardPresets
{
    public interface IBoardPresets
    {
        static abstract Board Init(int boardXSize, int boardYSize);
    }
}