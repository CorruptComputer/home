using Microsoft.AspNetCore.Components;

namespace Home.Components;

public partial class SkillsList : ComponentBase
{
    [Parameter]
    public required RenderFragment ChildContent { get; set; }
}
