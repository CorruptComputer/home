namespace Home.Pages.Blog.Posts.Drafts;

public partial class Test : IPost
{
    public static BlogPostMetaData GetMetaData()
    {
        return new()
        {
            Title = "Test",
            Excerpt = "Test 12345",
            Slug = "2025-07-01-test",
            PublishedDate = null,
            LastUpdatedDate = DateTimeOffset.Parse("2025-07-01T21:00:00-05:00"),
            ImageUrl = null,
            Edits = []
        };
    }
}
