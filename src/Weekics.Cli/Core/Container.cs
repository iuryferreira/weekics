using Microsoft.Extensions.DependencyInjection;
using Weekics.Cli.Core;
using Weekics.Cli.Core.Helpers;

namespace Weekics.Cli;

public static class Container
{
    private static System.IServiceProvider? ServiceProvider;

    public static System.IServiceProvider Build()
    {
        var services = new ServiceCollection();

        services.AddLogging(builder => builder.AddConsole());

        services.AddScoped<IIcsService, IcsService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IFileReader, FileReader>();

        ServiceProvider = services.BuildServiceProvider();
        return ServiceProvider;
    }

    public static T GetService<T>() where T: notnull => ServiceProvider!.GetRequiredService<T>();

}