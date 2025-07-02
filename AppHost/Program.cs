namespace AppHost;

public static class Program
{
    public static async Task Main(string[] args)
    {
        await DistributedApplication.CreateBuilder(args).BuildAppHost().RunAppHostAsync();
    }

    private static DistributedApplication BuildAppHost(this IDistributedApplicationBuilder builder)
    {
        builder.AddProject<Projects.Home>("Home").WithExternalHttpEndpoints();

        return builder.Build();
    }

    private static async Task RunAppHostAsync(this DistributedApplication app)
    {
        await app.RunAsync();
    }
}