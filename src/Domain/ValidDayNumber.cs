namespace AOC2022.Domain;

public readonly record struct ValidDayNumber
{
    public const int Max = 25;
    public const int Min = 1;
    public ValidDayNumber(int value)
    {
        IsInRange(value, Min, Max);

        Value = value;
    }

    public static ValidDayNumber FromString(string value)
    {
        var parsedValue = int.Parse(value);

        return new ValidDayNumber(parsedValue);
    }

    public int Value { get; }

    public static implicit operator int(ValidDayNumber validDayNumber) =>
        validDayNumber.Value;
}