using MarkdownSharp;
using Microsoft.AspNetCore.Components;

namespace Home.Pages.Blog;

public partial class Post(BlogPostService blogPostService)
{
    [Parameter]
    public required string Slug { get; set; }

    public BlogPostMetaData? MetaData { get; set; }

    public MarkupString? PostContent { get; set; }

    protected override async Task OnInitializedAsync()
    {
        MetaData = await blogPostService.GetPostMetaData(Slug);

        if (MetaData == null)
        {
            return;
        }

        string? postMarkdown = await blogPostService.GetPostMarkdown(MetaData.Slug);

        if (postMarkdown == null)
        {
            return;
        }

        PostContent = new(new Markdown().Transform(postMarkdown));

        await base.OnInitializedAsync();
    }
}
