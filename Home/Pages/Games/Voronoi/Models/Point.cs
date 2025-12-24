namespace Home.Pages.Games.Voronoi.Models;

public class Point
{
    public required int X { get; set; }
    public required int Y { get; set; }
    public string? Color { get; set; }

    public int Distance(int x, int y, bool manhattanDistance)
    {
        return manhattanDistance
            ? Math.Abs(x - X) + Math.Abs(y - Y)
            : (int)Math.Sqrt(Math.Pow(x - X, 2) + Math.Pow(y - Y, 2));
    }
}
