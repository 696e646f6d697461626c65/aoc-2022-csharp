using System.Diagnostics;

namespace AOC2022.Domain;

public abstract class WeaponOfChoice
{
    public WeaponOfChoice(int value)
    {
        Value = value;
    }

    public int Value { get; }
}

public static class WeaponOfChoiceExtensions
{
    public static WeaponOfChoice ParseAsRockPaperOrScissors(this char choice)
    {
        return choice switch
        {
            'X' or
            'A' => Rock.Instance,
            'Y' or
            'B' => Paper.Instance,
            'Z' or
            'C' => Scissors.Instance,
            _ => throw new UnreachableException("No such input.")
        };
    }

    public static int DetermineNaiveScoreAgainst(this WeaponOfChoice playerChoice, WeaponOfChoice opponentChoice)
    {
        const int winScore = 6;
        const int loseScore = 0;
        const int drawScore = 3;

        var roundScore = (playerChoice, opponentChoice) switch
        {
            (Rock, Scissors) or
            (Paper, Rock) or
            (Scissors, Paper) => winScore,
            (Rock, Paper) or
            (Paper, Scissors) or
            (Scissors, Rock) => loseScore,
            _ => drawScore
        };

        return roundScore + playerChoice.Value;
    }
}

public static class StrategyExtensions
{
    public static Strategy ParseAsStrategy(this char choice)
    {
        return choice switch
        {
            'X' => PlayToLose.Instance,
            'Y' => PlayToDraw.Instance,
            'Z' => PlayToWin.Instance,
            _ => throw new UnreachableException("No such input.")
        };
    }

    public static int CounterWith(
        this WeaponOfChoice opponent,
        Strategy counter)
    {
        var counterChoice = (opponent, counter) switch
        {
            (Rock, PlayToWin) => Paper.Instance,
            (Scissors, PlayToWin) => Rock.Instance,
            (Paper, PlayToWin) => Scissors.Instance,
            (Rock, PlayToLose) => Scissors.Instance,
            (Paper, PlayToLose) => Rock.Instance,
            (Scissors, PlayToLose) => Paper.Instance,
            _ => opponent
        };

        return counterChoice.Value + counter.Value;
    }
}

public abstract class Strategy
{
    public Strategy(int value)
    {
        Value = value;
    }

    public int Value { get; }
}

public class PlayToWin : Strategy
{
    private PlayToWin(int value) : base(value) { }

    public static PlayToWin Instance { get; } = new PlayToWin(6);
}

public class PlayToLose : Strategy
{
    private PlayToLose(int value) : base(value) { }

    public static PlayToLose Instance { get; } = new PlayToLose(0);
}

public class PlayToDraw : Strategy
{
    private PlayToDraw(int value) : base(value) { }

    public static PlayToDraw Instance { get; } = new PlayToDraw(3);
}

public class Rock : WeaponOfChoice
{
    private Rock(int value) : base(value)
    {

    }

    public static Rock Instance { get; } = new Rock(1);

}

public class Paper : WeaponOfChoice
{
    private Paper(int value) : base(value)
    {

    }

    public static Paper Instance { get; } = new Paper(2);
}

public class Scissors : WeaponOfChoice
{
    private Scissors(int value) : base(value)
    {

    }

    public static Scissors Instance { get; } = new Scissors(3);
}

public static class Referee
{

}