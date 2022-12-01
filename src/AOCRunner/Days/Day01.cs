using AOC2022.Domain;
using AOC2022.Domain.Common;

namespace AOC2022.ConsoleRunner.Days;

public class Day01 : AdventOfCodeDay
{
    private readonly IInputRetriever _retriever;

    public override required ValidDayNumber Number { get; init; }

    public Day01(IInputRetriever retriever)
    {
        Number = new ValidDayNumber(1);
        _retriever = retriever;
    }

    protected override Task<string> SolveTaskOne()
    {
        return Task.FromResult("Not yet");
    }

    protected override Task<string> SolveTaskTwo()
    {
        return Task.FromResult("Not yet");
    }
}