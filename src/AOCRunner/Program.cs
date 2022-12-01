using System.Reflection;

using AOC2022.Domain;
using AOC2022.Utilities;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

var cts = GetCancelKeySource();

var config = GetConfiguration();

var diContainer = GetDiContainer(config);

var inputDay = args[0];

var dayNumber = ValidDayNumber.FromString(inputDay);

var allStartedDays = diContainer
    .GetRequiredService<IEnumerable<AdventOfCodeDay>>()
    .ToList();

var dayToRun = allStartedDays?
    .Where(
        d => d.Number == dayNumber)
    .SingleOrDefault();

if (dayToRun is null)
{
    Console.WriteLine("Oh no! This day hasn't been started yet!");
    Console.WriteLine("Days started: ");
    allStartedDays!.ForEach(Console.WriteLine);
    return;
}

Console.WriteLine(await dayToRun.GetResult());

static CancellationTokenSource GetCancelKeySource()
{
    var cts = new CancellationTokenSource();
    Console.CancelKeyPress += (s, e) => cts.Cancel();
    return cts;
}

static IConfigurationRoot GetConfiguration()
{
    var builder = new ConfigurationBuilder();

    builder.AddUserSecrets(Assembly.GetEntryAssembly()!);

    builder.AddEnvironmentVariables("AOC2022_");

    return builder.Build();
}

static ServiceProvider GetDiContainer(IConfigurationRoot config)
{
    var sc = new ServiceCollection();

    sc.AddInputRetrievers(config);

    var days = AssemblyScanning.RetrieveSubclassesOf<AdventOfCodeDay>();

    foreach (var day in days)
    {
        sc.Add(new ServiceDescriptor(typeof(AdventOfCodeDay), day, ServiceLifetime.Transient));
    }

    var sp = sc.BuildServiceProvider();
    return sp;
}