namespace AOC2022.Domain;

public readonly record struct FoodCarryingElf : IComparable<FoodCarryingElf>
{
    public FoodCarryingElf(int calories)
    {
        IsInRange(calories, 0, int.MaxValue);

        Calories = calories;
    }

    public int Calories { get; }

    public int CompareTo(FoodCarryingElf other)
    {
        return Calories == other.Calories ? 0 : Calories > other.Calories ? 1 : -1;
    }
}