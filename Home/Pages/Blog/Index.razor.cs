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

    protected override async Task OnInitializedAsync()
    {
        BlogPosts = await blogPostService.GetPosts();
        await PreloadCurrentPagePosts();

        await InvokeAsync(StateHasChanged);
        base.OnInitialized();
    }

    protected override async Task OnParametersSetAsync()
    {
        await PreloadCurrentPagePosts();

        await base.OnParametersSetAsync();
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

    public static string GetPostUrlFromSlug(string slug)
    {
        return SiteUrls.BlogPost.Replace(SiteUrls.POST_SLUG_PARAM, slug);
    }

    private async Task PreloadCurrentPagePosts()
    {
        if (BlogPosts is null)
        {
            return;
        }

        int startIndex = CurrentPage * PostsPerPage;
        int endIndex = Math.Min(startIndex + PostsPerPage, BlogPosts.Count());

        IEnumerable<string> slugsToPreload = BlogPosts.Skip(startIndex).Take(endIndex - startIndex).Select(x => x.Slug);
        await blogPostService.PreloadPostMarkdown(slugsToPreload);
    }
}
