namespace Home.Pages.Blog.Posts._2019;

public partial class Windows10Privacy : IPost
{
    public static BlogPostMetaData GetMetaData()
    {
        return new()
        {
            Title = "Privacy and Windows 10",
            Excerpt = "Windows 10 has been a nightmare from a privacy perspective, however it doesn't have to be.",
            Slug = "2019-03-14-privacy-and-windows-10",
            PublishedDate = DateTimeOffset.Parse("2019-03-14T14:00:00-05:00"),
            LastUpdatedDate = DateTimeOffset.Parse("2022-07-02T23:00:00-05:00"),
            ImageUrl = null,
            Edits = []
        };
    }
}
