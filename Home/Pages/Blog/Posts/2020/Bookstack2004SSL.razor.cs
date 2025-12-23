namespace Home.Pages.Blog.Posts._2020;

public partial class Bookstack2004SSL : IPost
{
    public static BlogPostMetaData GetMetaData()
    {
        return new()
        {
            Title = "Installing BookStack on Ubuntu Server 20.04 with SSL",
            Excerpt = "BookStack is a free and open source Wiki software.",
            Slug = "2020-05-21-ubuntu-20.04-bookstack",
            PublishedDate = DateTimeOffset.Parse("2020-05-21T11:00:00-05:00"),
            LastUpdatedDate = DateTimeOffset.Parse("2025-12-23T21:00:00-05:00"),
            ImageUrl = null,
            Edits = new()
            {
                [DateOnly.Parse("2025-12-23")] = "Update to new blog post format."
            }
        };
    }
}
