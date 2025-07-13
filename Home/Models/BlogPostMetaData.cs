namespace Home.Models;

public class BlogPostMetaData
{
    public required string Title { get; set; }

    public required string Excerpt { get; set; }

    public required string Slug { get; set; }

    /// <summary>
    ///   Null if the post is a draft.
    /// </summary>
    public DateTimeOffset? PublishedDate { get; set; }

    public DateTimeOffset LastUpdatedDate { get; set; }

    public string? ImageUrl { get; set; }

    public Dictionary<DateOnly, string>? Edits { get; set; }
}
