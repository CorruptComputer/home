namespace Home.Pages.Blog.Posts._2017;

public partial class Linux : IPost
{
    public static BlogPostMetaData GetMetaData()
    {
        return new()
        {
            Title = "My Adventures with Linux",
            Excerpt = "I love Linux, but there are so many different distributions. So how can I know if my current one is my favorite if I don't try them all?",
            Slug = "2017-04-26-linux",
            PublishedDate = DateTimeOffset.Parse("2017-04-26T16:42:00-05:00"),
            LastUpdatedDate = DateTimeOffset.Parse("2025-12-23T21:00:00-05:00"),
            ImageUrl = "/images/blog/2017-04-26-linux/GNOMEdesktop.png",
            Edits = new()
            {
                [DateOnly.Parse("2017-04-26")] = "Grammar, spelling, and sources.",
                [DateOnly.Parse("2021-03-10")] = "Remove Antergos link. RIP :(",
                [DateOnly.Parse("2025-12-23")] = "Update to new blog post format."
            }
        };
    }
}
