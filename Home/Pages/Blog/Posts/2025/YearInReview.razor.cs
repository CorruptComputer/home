namespace Home.Pages.Blog.Posts._2025;

public partial class YearInReview : IPost
{
    public static BlogPostMetaData GetMetaData()
    {
        return new()
        {
            Title = "2025 Year In Review",
            Excerpt = "A look back through the projects and other things I've worked on throughout 2025.",
            Slug = "2025-12-25-year-in-review",
            PublishedDate = DateTimeOffset.Parse("2025-12-25T16:00:00-05:00"),
            LastUpdatedDate = DateTimeOffset.Parse("2025-12-25T16:00:00-05:00"),
            ImageUrl = null,
            Edits = []
        };
    }
}
