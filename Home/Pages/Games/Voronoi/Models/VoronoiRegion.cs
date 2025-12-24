namespace Home.Pages.Games.Voronoi.Models;

public class VoronoiRegion
{
    public required Point VoronoiPoint { get; set; }
    public required List<Point> PolygonPoints { get; set; }
}
