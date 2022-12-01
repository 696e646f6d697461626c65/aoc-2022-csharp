namespace AOC2022.Domain;

public readonly record struct DayResult
{
    public DayResult(ValidDayNumber day, string taskOneAnswer, string taskTwoAnswer)
    {
        Day = day;
        TaskOneAnswer = taskOneAnswer;
        TaskTwoAnswer = taskTwoAnswer;
    }

    public int Day { get; }
    public string TaskOneAnswer { get; } = "Not answered.";
    public string TaskTwoAnswer { get; } = "Not answered.";
}