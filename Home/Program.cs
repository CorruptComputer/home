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

        // HttpClient for accessing files in wwwroot
        builder.Services.AddHttpClient(SELF_HTTP_CLIENT_NAME, client =>
        {
            client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        });
        builder.Services.AddSingleton(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(SELF_HTTP_CLIENT_NAME));

        // Services
        builder.Services.AddSingleton<BlogPostService>();

        await builder.Build().RunAsync();
    }
}
