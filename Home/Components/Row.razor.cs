using Microsoft.AspNetCore.Components;

namespace Home.Components;

public partial class Row : ComponentBase
{
    [Parameter]
    public required RenderFragment ChildContent { get; set; }
}
