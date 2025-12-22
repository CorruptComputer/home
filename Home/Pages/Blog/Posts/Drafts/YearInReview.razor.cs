namespace Home.Pages.Blog.Posts.Drafts;

public partial class YearInReview : IPost
{
    public static BlogPostMetaData GetMetaData()
    {
        return new()
        {
            Title = "2025 Year In Review",
            Excerpt = "Test 12345",
            Slug = "2025-12-20-year-in-review",
            PublishedDate = null,
            LastUpdatedDate = DateTimeOffset.Parse("2025-12-20T21:00:00-05:00"),
            ImageUrl = null,
            Edits = []
        };
    }
}
