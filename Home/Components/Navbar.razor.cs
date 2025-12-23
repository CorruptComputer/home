using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Home.Components;

public partial class Navbar(NavigationManager NavManager) : ComponentBase
{
    public NavButton? SelectedNavButton { get; set; }

    protected bool Spinning { get; set; } = false;

    protected override void OnInitialized()
    {
        SetSelectedNavButton();

        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        SetSelectedNavButton();

        base.OnParametersSet();
    }

    protected void MonkeyOnClick(MouseEventArgs e)
    {
        if (!Spinning)
        {
            Spinning = true;
            Task.Delay(1000).ContinueWith(_ =>
            {
                Spinning = false;
                InvokeAsync(StateHasChanged);
            });
        }
    }

    protected void NavigateToHome()
    {
        SelectedNavButton = NavButton.Home;
        NavManager.NavigateTo("/");
    }

    protected void NavigateToBlog()
    {
        SelectedNavButton = NavButton.Blog;
        NavManager.NavigateTo("/blog");
    }

    protected void NavigateToGames()
    {
        if (SelectedNavButton == NavButton.Games)
        {
            return;
        }

        SelectedNavButton = NavButton.Games;
        NavManager.NavigateTo("/games/nim");
    }

    // For legacy blog URLs, /{Year}/{Month}/{Day}/{Slug}
    [GeneratedRegex(@"/\d{4}/\d{2}/\d{2}/[a-zA-Z0-9-]+")]
    private static partial Regex LegacyBlogPostUrlRegex();

    private void SetSelectedNavButton()
    {
        if (NavManager.Uri.Contains("/blog") || LegacyBlogPostUrlRegex().IsMatch(NavManager.Uri))
        {
            SelectedNavButton = NavButton.Blog;
        }
        else if (NavManager.Uri.Contains("/games"))
        {
            SelectedNavButton = NavButton.Games;
        }
        else
        {
            SelectedNavButton = NavButton.Home;
        }
    }

    public enum NavButton
    {
        Home,
        Blog,
        Games
    }
}
