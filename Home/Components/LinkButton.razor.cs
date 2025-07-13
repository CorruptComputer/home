using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Home.Components;

public partial class LinkButton : Button
{
    /// <summary>
    ///   The URL to navigate to when the button is clicked.
    /// </summary>
    [Parameter]
    [Url(ErrorMessage = "Must be a valid URL.")]
    public required string Href { get; set; }

    private readonly NavigationManager navManager;
    private readonly IJSRuntime jSRuntime;

    public LinkButton(NavigationManager navManager, IJSRuntime jSRuntime)
    {
        this.navManager = navManager;
        this.jSRuntime = jSRuntime;

        OnLeftClickCallback = EventCallback.Factory.Create(this, OnClick);
        OnMiddleClickCallback = EventCallback.Factory.Create(this, OnMiddleClickAsync);
    }

    protected void OnClick()
    {
        if (!Disabled)
        {
            navManager.NavigateTo(Href);
        }
    }

    protected async Task OnMiddleClickAsync()
    {
        if (!Disabled)
        {
            await jSRuntime.InvokeVoidAsync("open", Href, "_blank");
        }
    }
}