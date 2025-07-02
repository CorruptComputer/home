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
        SelectedNavButton = NavButton.Games;
        NavManager.NavigateTo("/games");
    }

    private void SetSelectedNavButton()
    {
        if (NavManager.Uri.Contains("/blog"))
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
