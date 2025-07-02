using System.Net.Http.Json;

namespace Home.Services;

public class BlogPostService(HttpClient httpClient)
{
    private IOrderedEnumerable<BlogPostMetaData>? BlogPosts { get; set; }

    private readonly List<ushort> Years = [2017, 2019, 2020, 2021, 2022];

    public async Task<IOrderedEnumerable<BlogPostMetaData>> GetPosts()
    {
        if (BlogPosts == null)
        {
            await LoadMetaData();
        }

        return BlogPosts ?? Enumerable.Empty<BlogPostMetaData>().OrderByDescending(x => x.PublishedDate);
    }

    public async Task<BlogPostMetaData?> GetPostMetaData(string slug)
    {
        if (BlogPosts == null)
        {
            await LoadMetaData();
        }

        return BlogPosts?.FirstOrDefault(x => x.Slug == slug);
    }

    public async Task<string?> GetPostMarkdown(string slug)
    {
        BlogPostMetaData? metaData = await GetPostMetaData(slug);

        if (metaData == null)
        {
            return null;
        }

        return await httpClient.GetStringAsync($"posts/{metaData.PublishedDate?.Year.ToString() ?? "drafts"}/{metaData.Slug}.md");
    }

    private async Task LoadMetaData()
    {
        IOrderedEnumerable<BlogPostMetaData> allPosts = Enumerable.Empty<BlogPostMetaData>().OrderByDescending(x => x.PublishedDate);

        foreach (ushort year in Years)
        {
            List<BlogPostMetaData>? posts = await httpClient.GetFromJsonAsync<List<BlogPostMetaData>>($"posts/{year}/metadata.json");

            if (posts != null)
            {
                allPosts = allPosts.Concat(posts).OrderByDescending(x => x.PublishedDate);
            }
        }

#if DEBUG
        List<BlogPostMetaData>? draftPosts = await httpClient.GetFromJsonAsync<List<BlogPostMetaData>>($"posts/drafts/metadata.json");

        if (draftPosts != null && draftPosts.Count != 0)
        {
            allPosts = allPosts.Concat(draftPosts)
                               .OrderByDescending(x => (x.PublishedDate == null ? DateTimeOffset.MaxValue : x.PublishedDate))
                               .ThenByDescending(x => x.LastUpdatedDate);
        }
#endif

        BlogPosts = allPosts;
    }
}
