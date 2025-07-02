using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace Home.Components;

public partial class Box : ComponentBase
{
    [Parameter]
    public required string Title { get; set; }

    [Parameter]
    public TitleType TitleType { get; set; } = TitleType.H2;

    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    [Parameter]
    public required DateTimeOffset? Date { get; set; }

    [Parameter]
    [RegularExpression(@"^#([0-9a-fA-F]{3}){1,2}$", ErrorMessage = "Must be a valid hex color code (e.g., #FFF, #ffffff).")]
    public string? BackgroundColorOverride { get; set; }

    private const string DateFormat = "MMMM d, yyyy: ";

    protected MarkupString TitleHtml => new(
            $@"<{TitleType.ToString().ToLowerInvariant()}>
                    {(Date == null ? string.Empty : Date.Value.ToString(DateFormat))}{Title}
               </{TitleType.ToString().ToLowerInvariant()}>"
        );
}


