

using AOC2022.Domain;
using AOC2022.Domain.Common;

public class Day02 : AdventOfCodeDay
{
    private readonly IInputRetriever _inputRetriever;

    public override required ValidDayNumber Number { get; init; }

    public Day02(IInputRetriever inputRetriever)
    {
        _inputRetriever = inputRetriever;
        Number = new ValidDayNumber(2);
    }

    protected override async Task<string> SolveTaskOne()
    {
        return await _inputRetriever
            .GetInputForDay(Number)
            .AggregateAsync(
                0,
                (tournamentScore, currentInput) =>
                {
                    var player = currentInput[2].ParseAsRockPaperOrScissors();
                    var opponent = currentInput[0].ParseAsRockPaperOrScissors();
                    return tournamentScore + player.DetermineNaiveScoreAgainst(opponent);
                },
                tournamentScore => tournamentScore.ToString());
    }

    protected override async Task<string> SolveTaskTwo()
    {
        return await _inputRetriever
            .GetInputForDay(Number)
            .AggregateAsync(
                0,
                (tournamentScore, currentInput) =>
                {
                    var strategy = currentInput[2].ParseAsStrategy();
                    var opponent = currentInput[0].ParseAsRockPaperOrScissors();
                    return tournamentScore + opponent.CounterWith(strategy);
                },
                (tournamentScore) => tournamentScore.ToString());
    }
}