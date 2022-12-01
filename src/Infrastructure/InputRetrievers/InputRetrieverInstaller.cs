using AOC2022.Domain.Common;
using AOC2022.Infrastructure.InputRetrievers;

using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class InputRetrieverInstaller
{
    public static IServiceCollection AddInputRetrievers(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddHttpInputRetriever(config);

        return services;
    }

    internal static IServiceCollection AddHttpInputRetriever(
        this IServiceCollection services,
        IConfiguration config)
    {
        var sessionCookie = config
                    .GetRequiredSection("HttpInputRetriever")?
                    .Get<HttpInputRetrieverConfiguration>()?
                    .SessionCookie;

        IsNotNullOrWhiteSpace(sessionCookie);

        services
            .AddHttpClient<HttpInputRetriever>()
            .ConfigureHttpClient(configure => configure.DefaultRequestHeaders
                .Add("Cookie", new string[] { sessionCookie }));

        services.AddTransient<IInputRetriever, HttpInputRetriever>(sp =>
            sp.GetRequiredService<HttpInputRetriever>());

        return services;
    }

}