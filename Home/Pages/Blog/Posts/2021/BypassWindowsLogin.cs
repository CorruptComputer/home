namespace Home.Pages.Blog.Posts._2021;

public partial class BypassWindowsLogin : IPost
{
    public static BlogPostMetaData GetMetaData()
    {
        return new()
        {
            Title = "Bypass Windows login with Sticky Keys",
            Excerpt = "I have known about this vulnerability for years, but recently I've figured out that this exploit is not very well known.",
            Slug = "2021-05-10-bypass-windows-login-with-sticky-keys",
            PublishedDate = DateTimeOffset.Parse("2021-05-10T15:00:00-05:00"),
            LastUpdatedDate = DateTimeOffset.Parse("2022-07-02T23:00:00-05:00"),
            ImageUrl = null,
            Edits = []
        };
    }
}
