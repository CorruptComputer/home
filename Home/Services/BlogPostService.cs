namespace Home.Services;

public class BlogPostService(List<BlogPostMetaData> posts)
{
    public async Task<IOrderedEnumerable<BlogPostMetaData>> GetPosts()
    {
#if DEBUG
            return posts
                .OrderByDescending(x => x.PublishedDate is null)
                .ThenByDescending(x => x.PublishedDate);
#else
            return posts
                .Where(x => x.PublishedDate is not null)
                .OrderByDescending(x => x.PublishedDate);
#endif
    }
}
