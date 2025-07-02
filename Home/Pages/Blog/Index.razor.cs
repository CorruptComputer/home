using Microsoft.AspNetCore.Components;

namespace Home.Pages.Blog;

public partial class Index(NavigationManager navManager, BlogPostService blogPostService)
{
    [Parameter]
    [SupplyParameterFromQuery(Name = "p")]
    public int CurrentPage { get; set; } = 0;

    public const int PostsPerPage = 4;

    public int TotalPages => BlogPosts?.Count() switch
    {
        null => 0,
        _ => (int)Math.Ceiling(BlogPosts.Count() / (double)PostsPerPage)
    };

    protected IOrderedEnumerable<BlogPostMetaData>? BlogPosts { get; set; }

    protected override async void OnInitialized()
    {
        BlogPosts = await blogPostService.GetPosts();

        await InvokeAsync(StateHasChanged);
        base.OnInitialized();
    }

    public void NavigateToPage(int page)
    {
        if (page < TotalPages || page > 0)
        {
            navManager.NavigateTo($"{SiteUrls.Blog}?p={page}");
        }
    }

    public void NavigateToNextPage()
    {
        if (CurrentPage < TotalPages)
        {
            navManager.NavigateTo($"{SiteUrls.Blog}?p={CurrentPage + 1}");
        }
    }

    public void NavigateToPreviousPage()
    {
        if (CurrentPage > 0)
        {
            navManager.NavigateTo($"{SiteUrls.Blog}?p={CurrentPage - 1}");
        }
    }

    public void NavigateToPost(string slug)
    {
        navManager.NavigateTo(SiteUrls.BlogPost.Replace(SiteUrls.POST_SLUG_PARAM, slug));
    }
}
