namespace AOC2022.Domain;

public abstract class AdventOfCodeDay
{
    public abstract required ValidDayNumber Number { get; init; }

    protected abstract Task<string> SolveTaskOne();

    protected abstract Task<string> SolveTaskTwo();

    public async Task<DayResult> GetResult()
    {
        return new DayResult(Number, await SolveTaskOne(), await SolveTaskTwo());
    }

    public override string ToString()
    {
        return Number.Value.ToString("D2");
    }
}