using Microsoft.AspNetCore.Components;

namespace Home.Pages;

public partial class LegacyBlogPostRedirect(NavigationManager navManager) : ComponentBase
{
    [Parameter]
    public string? Year { get; set; }

    [Parameter]
    public string? Month { get; set; }

    [Parameter]
    public string? Day { get; set; }

    [Parameter]
    public string? Slug { get; set; }

    protected override void OnInitialized()
    {
        if (string.IsNullOrWhiteSpace(Year) || !int.TryParse(Year, out _)
            || string.IsNullOrWhiteSpace(Month) || !int.TryParse(Month, out _)
            || string.IsNullOrWhiteSpace(Day) || !int.TryParse(Day, out _)
            || string.IsNullOrWhiteSpace(Slug))
        {
            navManager.NavigateTo("/blog");
            return;
        }

        navManager.NavigateTo($"/blog/{Year}-{Month}-{Day}-{Slug.Replace(".html", string.Empty)}");
    }
}