using Home.Components;
using Microsoft.AspNetCore.Components;

namespace Home.Layout;

public partial class MainLayout(NavigationManager navMan) : LayoutComponentBase
{
    private bool ShowGamesNavbar
        => navMan.Uri.Contains("/games");

    private string ContainerClass
        => ShowGamesNavbar ? "gamesContainer" : "container";
}
