using Home.Pages.Games.Voronoi.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Home.Pages.Games.Voronoi;

public partial class Index(IJSRuntime JS, ILogger<Index> logger) : ComponentBase
{
    private VoronoiGameState State { get; set; } = new();
    private List<VoronoiRegion> Regions { get; set; } = [];
    private int CanvasSize = 600;
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
            CanvasSize = (int)rect.Width;
            logger.LogInformation("SVG dimensions: {Width}x{Height}", CanvasSize, CanvasSize);
            StateHasChanged();
        }
    }

    private async Task OnCanvasClick(MouseEventArgs e)
    {
        // Get click position relative to SVG
        DOMRectData rect = await JS.InvokeAsync<DOMRectData>("eval", "document.querySelector('svg').getBoundingClientRect()");
        int x = (int)((e.ClientX - rect.Left) * CanvasSize / rect.Width);
        int y = (int)((e.ClientY - rect.Top) * CanvasSize / rect.Height);

        x = Math.Max(0, Math.Min(CanvasSize - 1, x));
        y = Math.Max(0, Math.Min(CanvasSize - 1, y));

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
            if (State.VoronoiPoints.Count == 0)
            {
                Regions.Clear();
                return;
            }

            // Compute regions on background thread
            Regions = await Task.Run(() => State.BuildVoronoiDiagram(CanvasSize));
            logger.LogInformation("Voronoi diagram computed with {RegionCount} regions.", Regions.Count);
        }
        finally
        {
            isUpdating = false;
            StateHasChanged();
        }
    }
}
