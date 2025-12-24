namespace Home.Pages.Games.Voronoi.Models;

public class VoronoiRegion
{
    public VoronoiPoint Point { get; set; } = null!;
    public List<(int x, int y)> PolygonPoints { get; set; } = [];
}
