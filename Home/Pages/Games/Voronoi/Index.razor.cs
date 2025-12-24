using Home.Pages.Games.Voronoi.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Home.Pages.Games.Voronoi;

public partial class Index(IJSRuntime JS, ILogger<Index> logger) : ComponentBase
{
    private VoronoiGameState State { get; set; } = new();
    private List<VoronoiRegion> Regions { get; set; } = [];
    private int CanvasWidth = 600;
    private int CanvasHeight = 600; // Keep square
    private const int GridResolution = 4; // Sample every 4 pixels for boundary detection
    private bool isUpdating = false;

    private class DOMRectData
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Measure the actual SVG size and use it for calculations
            DOMRectData rect = await JS.InvokeAsync<DOMRectData>("eval", "document.querySelector('svg').getBoundingClientRect()");
            CanvasWidth = (int)rect.Width;
            CanvasHeight = (int)rect.Height;
            logger.LogInformation("SVG dimensions: {Width}x{Height}", CanvasWidth, CanvasHeight);
            StateHasChanged();
        }
    }

    private async Task OnCanvasClick(MouseEventArgs e)
    {
        // Get click position relative to SVG
        DOMRectData rect = await JS.InvokeAsync<DOMRectData>("eval", "document.querySelector('svg').getBoundingClientRect()");
        int x = (int)((e.ClientX - rect.Left) * CanvasWidth / rect.Width);
        int y = (int)((e.ClientY - rect.Top) * CanvasHeight / rect.Height);

        x = Math.Max(0, Math.Min(CanvasWidth - 1, x));
        y = Math.Max(0, Math.Min(CanvasHeight - 1, y));

        logger.LogInformation("SVG clicked at ({X}, {Y})", x, y);
        State.AddPoint(x, y);
        await UpdateVoronoiDiagram();
    }

    private async Task OnManhattanToggle(ChangeEventArgs e)
    {
        State.ToggleDistanceMode();
        await UpdateVoronoiDiagram();
    }

    private async Task OnClear()
    {
        State.Clear();
        Regions.Clear();
        logger.LogInformation("Voronoi diagram cleared.");
        await Task.CompletedTask;
    }

    private async Task UpdateVoronoiDiagram()
    {
        if (isUpdating)
        {
            return;
        }

        isUpdating = true;
        try
        {
            if (State.GeneratingPoints.Count == 0)
            {
                Regions.Clear();
                return;
            }

            // Compute regions on background thread
            Regions = await Task.Run(() => ComputeVoronoiRegions());
            logger.LogInformation("Voronoi diagram computed with {RegionCount} regions.", Regions.Count);
        }
        finally
        {
            isUpdating = false;
            StateHasChanged();
        }
    }

    private List<VoronoiRegion> ComputeVoronoiRegions()
    {
        // Create a grid mapping each cell to its closest point
        int gridWidth = (CanvasWidth / GridResolution) + 1;
        int gridHeight = (CanvasHeight / GridResolution) + 1;
        VoronoiPoint[,] grid = new VoronoiPoint[gridWidth, gridHeight];

        // Sample the grid
        for (int gy = 0; gy < gridHeight; gy++)
        {
            for (int gx = 0; gx < gridWidth; gx++)
            {
                int sampleX = gx * GridResolution;
                int sampleY = gy * GridResolution;

                VoronoiPoint closestPoint = State.GeneratingPoints[0];
                int closestDist = closestPoint.Distance(sampleX, sampleY, State.ManhattanDistance);

                for (int i = 1; i < State.GeneratingPoints.Count; i++)
                {
                    int dist = State.GeneratingPoints[i].Distance(sampleX, sampleY, State.ManhattanDistance);
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closestPoint = State.GeneratingPoints[i];
                    }
                }

                grid[gx, gy] = closestPoint;
            }
        }

        // Find boundaries and create regions
        Dictionary<VoronoiPoint, List<(int x, int y)>> regions = [];
        HashSet<(int, int)> visited = [];

        for (int gy = 0; gy < gridHeight; gy++)
        {
            for (int gx = 0; gx < gridWidth; gx++)
            {
                VoronoiPoint point = grid[gx, gy];

                if (!regions.ContainsKey(point))
                {
                    regions[point] = [];
                }

                // Add the pixel coordinates that are on the boundary of this point
                int pixelX = gx * GridResolution;
                int pixelY = gy * GridResolution;

                // Check if this is a boundary cell (different from a neighbor)
                bool isBoundary = false;
                if (gx == 0 || gx == gridWidth - 1 || gy == 0 || gy == gridHeight - 1)
                {
                    isBoundary = true;
                }
                else if (gx > 0 && grid[gx - 1, gy] != point)
                {
                    isBoundary = true;
                }
                else if (gx < gridWidth - 1 && grid[gx + 1, gy] != point)
                {
                    isBoundary = true;
                }
                else if (gy > 0 && grid[gx, gy - 1] != point)
                {
                    isBoundary = true;
                }
                else if (gy < gridHeight - 1 && grid[gx, gy + 1] != point)
                {
                    isBoundary = true;
                }

                if (isBoundary && !visited.Contains((gx, gy)))
                {
                    regions[point].Add((pixelX, pixelY));
                    visited.Add((gx, gy));
                }
            }
        }

        // Convert to VoronoiRegion objects and compute final polygon boundaries
        List<VoronoiRegion> result = [];
        foreach ((VoronoiPoint? point, List<(int x, int y)>? pixels) in regions)
        {
            VoronoiRegion region = new()
            {
                Point = point
            };

            if (pixels.Count > 0)
            {
                // Sort them radially around the point's center
                List<(int x, int y)> sortedPixels = pixels
                    .OrderBy(p => Math.Atan2(p.y - point.Y, p.x - point.X))
                    .ToList();

                // Simplify the polygon to smooth jagged edges
                List<(int x, int y)> simplifiedPixels = SimplifyPolygon(sortedPixels, tolerance: 10);
                region.PolygonPoints = simplifiedPixels;
            }

            if (region.PolygonPoints.Count > 2)
            {
                result.Add(region);
            }
        }

        return result;
    }

    private List<(int x, int y)> SimplifyPolygon(List<(int x, int y)> points, double tolerance)
    {
        if (points.Count <= 2)
        {
            return points;
        }

        // Douglas-Peucker algorithm for polygon simplification
        double dmax = 0.0;
        int index = 0;

        for (int i = 1; i < points.Count - 1; i++)
        {
            double d = PointToLineDistance(points[i], points[0], points[^1]);
            if (d > dmax)
            {
                index = i;
                dmax = d;
            }
        }

        if (dmax > tolerance)
        {
            List<(int x, int y)> rec1 = SimplifyPolygon(points.GetRange(0, index + 1), tolerance);
            List<(int x, int y)> rec2 = SimplifyPolygon(points.GetRange(index, points.Count - index), tolerance);

            List<(int x, int y)> result = rec1.GetRange(0, rec1.Count - 1);
            result.AddRange(rec2);
            return result;
        }
        else
        {
            return [points[0], points[^1]];
        }
    }

    private double PointToLineDistance((int x, int y) point, (int x, int y) lineStart, (int x, int y) lineEnd)
    {
        int dx = lineEnd.x - lineStart.x;
        int dy = lineEnd.y - lineStart.y;

        if (dx == 0 && dy == 0)
        {
            return Math.Sqrt(Math.Pow(point.x - lineStart.x, 2) + Math.Pow(point.y - lineStart.y, 2));
        }

        int t = ((point.x - lineStart.x) * dx + (point.y - lineStart.y) * dy) / (dx * dx + dy * dy);
        t = Math.Max(0, Math.Min(1, t));

        int closestX = lineStart.x + t * dx;
        int closestY = lineStart.y + t * dy;

        return Math.Sqrt(Math.Pow(point.x - closestX, 2) + Math.Pow(point.y - closestY, 2));
    }
}
