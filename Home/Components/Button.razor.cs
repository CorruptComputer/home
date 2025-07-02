
using Microsoft.AspNetCore.Components;

namespace Home.Components;

public partial class Button : ComponentBase
{
    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    [Parameter]
    public required EventCallback OnClick { get; set; }

    [Parameter]
    public bool Disabled { get; set; } = false;
}
