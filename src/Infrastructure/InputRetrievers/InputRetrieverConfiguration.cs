using System.Reflection.Metadata.Ecma335;

namespace AOC2022.Infrastructure.InputRetrievers;

public class InputRetrieverConfiguration
{
    public const string Name = "InputRetrievers";
    public bool UseSampleInputRetriever { get; init; } = false;
    public required HttpInputRetrieverConfiguration Http { get; init; }
}