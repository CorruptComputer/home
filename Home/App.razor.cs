using System;
using Microsoft.AspNetCore.Components;

namespace Home;

public partial class App(NavigationManager navManager)
{
    private void NavigateHome()
    {
        navManager.NavigateTo("/");
    }
}
