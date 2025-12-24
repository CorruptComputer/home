namespace Home.Pages.Games.Voronoi.Models;

public class VoronoiGameState
{
    private const int MaxPoints = 20;
    public List<Point> VoronoiPoints { get; } = [];
    public bool ManhattanDistance { get; set; } = false;

    public void AddPoint(int x, int y)
    {
        if (VoronoiPoints.Count >= MaxPoints)
        {
            VoronoiPoints.RemoveAt(0);
        }

        VoronoiPoints.Add(new Point
        {
            X = x,
            Y = y,
            Color = GenerateRandomColor()
        });
    }

    public void Clear()
    {
        VoronoiPoints.Clear();
    }

    public void ToggleDistanceMode()
    {
        ManhattanDistance = !ManhattanDistance;
    }

    public List<VoronoiRegion> BuildVoronoiDiagram(int canvasSize)
    {
        List<DPoint> sites = [.. VoronoiPoints.Select(p => new DPoint(p.X, p.Y))];

        List<DPoint> canvas = CreateCanvas(canvasSize);

        List<VoronoiRegion> regions = [];

        for (int i = 0; i < sites.Count; i++)
        {
            List<DPoint> poly = [.. canvas];

            for (int j = 0; j < sites.Count; j++)
            {
                if (i == j)
                {
                    continue;
                }

                poly = ClipEuclidean(poly, sites[i], sites[j]);

                if (poly.Count == 0)
                {
                    break;
                }
            }

            regions.Add(new VoronoiRegion
            {
                VoronoiPoint = VoronoiPoints[i],
                PolygonPoints = [.. poly
                    .Select(p => new Point
                    {
                        X = (int)Math.Round(p.X),
                        Y = (int)Math.Round(p.Y)
                    })]
            });
        }

        return regions;
    }

    private static string GenerateRandomColor()
    {
        int r = Random.Shared.Next(256);
        int g = Random.Shared.Next(256);
        int b = Random.Shared.Next(256);
        return $"rgb({r}, {g}, {b})";
    }

    private static List<DPoint> CreateCanvas(int size)
    {
        return
        [
            new(0, 0),
            new(size, 0),
            new(size, size),
            new(0, size)
        ];
    }

    private const double Epsilon = 1e-9;

    private static List<DPoint> ClipEuclidean(
        List<DPoint> polygon,
        DPoint keep,
        DPoint other)
    {
        DPoint mid = (keep + other) * 0.5;
        DPoint normal = other - keep;

        bool Inside(DPoint p)
        {
            double dx = p.X - mid.X;
            double dy = p.Y - mid.Y;
            return (dx * normal.X + dy * normal.Y) <= Epsilon;
        }

        return ClipPolygon(polygon, Inside,
            (a, b) => Intersect(a, b, mid, normal));
    }

    private static List<DPoint> ClipPolygon(
        List<DPoint> poly,
        Func<DPoint, bool> inside,
        Func<DPoint, DPoint, DPoint> intersect)
    {
        List<DPoint> result = [];

        for (int i = 0; i < poly.Count; i++)
        {
            DPoint a = poly[i];
            DPoint b = poly[(i + 1) % poly.Count];

            bool aIn = inside(a);
            bool bIn = inside(b);

            if (aIn && bIn)
            {
                result.Add(b);
            }
            else if (aIn && !bIn)
            {
                result.Add(intersect(a, b));
            }
            else if (!aIn && bIn)
            {
                result.Add(intersect(a, b));
                result.Add(b);
            }
        }

        return result;
    }

    private static DPoint Intersect(
        DPoint a,
        DPoint b,
        DPoint mid,
        DPoint normal)
    {
        DPoint ab = b - a;
        double t =
            ((mid.X - a.X) * normal.X +
            (mid.Y - a.Y) * normal.Y) /
            (ab.X * normal.X + ab.Y * normal.Y);

        return new DPoint(a.X + ab.X * t, a.Y + ab.Y * t);
    }
}
