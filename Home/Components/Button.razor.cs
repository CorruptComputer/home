using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Home.Components;

public partial class Button : ComponentBase
{
    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    [Parameter]
    public required EventCallback OnLeftClickCallback { get; set; }

    [Parameter]
    public EventCallback OnMiddleClickCallback { get; set; } = new();

    [Parameter]
    public bool Disabled { get; set; } = false;

    private async Task OnMouseDownAsync(EventArgs e)
    {
        if (!Disabled && e is MouseEventArgs mouseEventArgs)
        {
            Task task = mouseEventArgs.Button switch
            {
                0 => OnLeftClickCallback.InvokeAsync(), // Left
                1 => OnMiddleClickCallback.InvokeAsync(), // Middle
                _ => Task.CompletedTask
            };

            await task;
        }
    }
}
