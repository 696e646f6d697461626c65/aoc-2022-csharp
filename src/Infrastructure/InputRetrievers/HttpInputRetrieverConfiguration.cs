namespace AOC2022.Infrastructure.InputRetrievers;

public class HttpInputRetrieverConfiguration
{
    public const string Name = "Http";
    public required string SessionCookie { get; init; }
}