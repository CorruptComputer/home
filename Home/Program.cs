using System.Reflection;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Home;

public static class Program
{
    private const string SELF_HTTP_CLIENT_NAME = "Home";
    public static async Task Main(string[] args)
    {
        WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        // Services
        // Find all IPost implementations and add their metadata to this list
        List<BlogPostMetaData> allMetaData = [];

        Assembly assembly = Assembly.GetExecutingAssembly();
        Type iPostType = typeof(Pages.Blog.Posts.IPost);
        foreach (Type type in assembly.GetTypes())
        {
            if (iPostType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            {
                MethodInfo? method = type.GetMethod(
                    nameof(Pages.Blog.Posts.IPost.GetMetaData), BindingFlags.Static | BindingFlags.Public
                );

                if (method?.Invoke(null, null) is BlogPostMetaData metadata)
                {
                    allMetaData.Add(metadata);
                }
            }
        }

        builder.Services.AddSingleton(new BlogPostService(allMetaData));

        await builder.Build().RunAsync();
    }
}
