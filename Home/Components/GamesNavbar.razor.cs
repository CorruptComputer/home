using Microsoft.AspNetCore.Components;

namespace Home.Components;

public partial class GamesNavbar(NavigationManager NavManager) : ComponentBase
{
    public GameButton SelectedGameButton { get; set; } = GameButton.Nim;

    protected override void OnInitialized()
    {
        GetSelectedGameButton();
        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        GetSelectedGameButton();
        base.OnParametersSet();
    }

    protected void NavigateToNotNim()
    {
        SelectedGameButton = GameButton.Nim;
        NavManager.NavigateTo("/games/nim");
    }

    protected void NavigateToVoronoi()
    {
        SelectedGameButton = GameButton.Voronoi;
        NavManager.NavigateTo("/games/voronoi");
    }

    private void GetSelectedGameButton()
    {
        if (NavManager.Uri.Contains("/games/nim"))
        {
            SelectedGameButton = GameButton.Nim;
        }
        else if (NavManager.Uri.Contains("/games/voronoi"))
        {
            SelectedGameButton = GameButton.Voronoi;
        }
    }

    public enum GameButton
    {
        Nim,
        Voronoi
    }
}