namespace Home.Models;

public sealed record BlogPostMetaData
{
    public required string Title { get; init; }

    public required string Excerpt { get; init; }

    public required string Slug { get; init; }

    /// <summary>
    ///   Null if the post is a draft.
    /// </summary>
    public required DateTimeOffset? PublishedDate { get; init; }

    public required DateTimeOffset LastUpdatedDate { get; init; }

    public required string? ImageUrl { get; init; }

    public required Dictionary<DateOnly, string> Edits { get; init; }
}
