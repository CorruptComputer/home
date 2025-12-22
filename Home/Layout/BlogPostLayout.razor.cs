using System.Reflection;
using Home.Pages.Blog.Posts;
using Microsoft.AspNetCore.Components;

namespace Home.Layout;

public partial class BlogPostLayout(NavigationManager navMan) : LayoutComponentBase
{
    private BlogPostMetaData? MetaData { get; set; }

    override protected void OnInitialized()
    {
        if (Body?.Target is RouteView rv)
        {
            if (rv.RouteData.PageType.GetInterface(nameof(IPost)) is not null)
            {
                // Call the static method directly on the type
                MethodInfo? method = rv.RouteData.PageType.GetMethod(
                    nameof(IPost.GetMetaData), BindingFlags.Static | BindingFlags.Public
                );

                if (method?.Invoke(null, null) is BlogPostMetaData metadata)
                {
                    MetaData = metadata;
                }
            }
        }

        // Was not a valid Blog Post
        if (MetaData == null)
        {
            navMan.NavigateTo("/blog");
            return;
        }

        base.OnInitialized();
    }
}
