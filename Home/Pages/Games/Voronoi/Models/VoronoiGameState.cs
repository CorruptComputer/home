namespace Home.Pages.Games.Voronoi.Models;

public class VoronoiGameState
{
    private const int MaxPoints = 20;
    public List<VoronoiPoint> GeneratingPoints { get; } = [];
    public bool ManhattanDistance { get; set; } = false;

    public void AddPoint(int x, int y)
    {
        if (GeneratingPoints.Count >= MaxPoints)
        {
            GeneratingPoints.RemoveAt(0);
        }

        GeneratingPoints.Add(new VoronoiPoint
        {
            X = x,
            Y = y,
            Color = GenerateRandomColor()
        });
    }

    public void Clear()
    {
        GeneratingPoints.Clear();
    }

    public void ToggleDistanceMode()
    {
        ManhattanDistance = !ManhattanDistance;
    }

    private static string GenerateRandomColor()
    {
        int r = Random.Shared.Next(256);
        int g = Random.Shared.Next(256);
        int b = Random.Shared.Next(256);
        return $"rgb({r}, {g}, {b})";
    }
}
