using AOC2022.Domain.Common;
using AOC2022.Infrastructure.InputRetrievers;

using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class InputRetrieverInstaller
{
    public static IServiceCollection AddInputRetriever(
        this IServiceCollection services,
        IConfiguration config)
    {
        var shouldUseSampleInputRetriever = config
            .GetRequiredSection(InputRetrieverConfiguration.Name)
                .Get<InputRetrieverConfiguration>()!
                .UseSampleInputRetriever;

        if (shouldUseSampleInputRetriever)
        {
            services.AddSampleInputRetriever();
            return services;
        }

        services.AddHttpInputRetriever(config);

        return services;
    }

    public static IServiceCollection AddSampleInputRetriever(
        this IServiceCollection services)
    {
        services.AddSingleton<IInputRetriever, SampleInputRetriever>();
        return services;
    }

    public static IServiceCollection AddHttpInputRetriever(
        this IServiceCollection services,
        IConfiguration config)
    {
        var sessionCookie = config
            .GetRequiredSection(InputRetrieverConfiguration.Name)
                .GetRequiredSection(HttpInputRetrieverConfiguration.Name)?
                    .Get<HttpInputRetrieverConfiguration>()?
                    .SessionCookie;

        IsNotNullOrWhiteSpace(sessionCookie);

        services
            .AddHttpClient<HttpInputRetriever>()
            .ConfigureHttpClient(
                (configure) =>
                    configure.DefaultRequestHeaders
                        .Add("Cookie", new string[] { sessionCookie }));

        services.AddTransient<IInputRetriever, HttpInputRetriever>(sp =>
            sp.GetRequiredService<HttpInputRetriever>());

        return services;
    }

}