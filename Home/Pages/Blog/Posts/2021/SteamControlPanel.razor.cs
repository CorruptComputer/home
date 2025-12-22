namespace Home.Pages.Blog.Posts._2021;

public partial class SteamControlPanel : IPost
{
    public static BlogPostMetaData GetMetaData()
    {
        return new()
        {
            Title = "Removing Steam Games from Control Panel and Apps list on Windows",
            Excerpt = "It might just be me, but I find these to be absolutely useless and actually get in the way of actually finding the apps that I want to remove.",
            Slug = "2021-03-05-remove-steam-games-from-control-panel",
            PublishedDate = DateTimeOffset.Parse("2021-03-05T23:00:00-05:00"),
            LastUpdatedDate = DateTimeOffset.Parse("2022-07-02T23:00:00-05:00"),
            ImageUrl = null,
            Edits = []
        };
    }
}
