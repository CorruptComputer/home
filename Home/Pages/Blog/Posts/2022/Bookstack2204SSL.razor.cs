namespace Home.Pages.Blog.Posts._2022;

public partial class Bookstack2204SSL : IPost
{
    public static BlogPostMetaData GetMetaData()
    {
        return new()
        {
            Title = "Installing BookStack on Ubuntu Server 22.04 with SSL",
            Excerpt = "BookStack is a free and open source Wiki software. Lets get it setup on Ubuntu Server 22.04 with SSL enabled.",
            Slug = "2022-07-02-ubuntu-22.04-bookstack",
            PublishedDate = DateTimeOffset.Parse("2022-07-02T23:00:00-05:00"),
            LastUpdatedDate = DateTimeOffset.Parse("2025-12-23T21:00:00-05:00"),
            ImageUrl = null,
            Edits = new()
            {
                [DateOnly.Parse("2025-12-23")] = "Update to new blog post format."
            }
        };
    }
}
