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

    private ValueTask<List<FoodCarryingElf>> SortFoodCarryingElves()
    {
        var elvesByCalories = new List<FoodCarryingElf>();
        return _retriever.GetInputForDay(Number)
            .AggregateAsync(
                (elvesByCalories, currentElf: new FoodCarryingElf(calories: 0)),
                (accumulator, calories) =>
                {
                    if (string.IsNullOrWhiteSpace(calories))
                    {
                        accumulator.elvesByCalories.Add(accumulator.currentElf);
                        accumulator.currentElf = new FoodCarryingElf(calories: 0);
                        return accumulator;
                    }
                    accumulator.currentElf = new FoodCarryingElf(
                        int.Parse(calories) + accumulator.currentElf.Calories);
                    return accumulator;
                },
                accumulator =>
                {
                    accumulator.elvesByCalories.Add(accumulator.currentElf);
                    return accumulator.elvesByCalories;
                });
    }

    protected async override Task<string> SolveTaskOne()
    {
        return (await SortFoodCarryingElves())
            .Max().Calories.ToString();
    }

    protected override async Task<string> SolveTaskTwo()
    {
        return (await SortFoodCarryingElves())
            .OrderDescending()
            .Take(3)
            .Select(e => e.Calories)
            .Aggregate(
                0,
                (totalCalories, elfCalories) =>
                {
                    Console.WriteLine($"total: {totalCalories}, current: {elfCalories}");
                    return totalCalories + elfCalories;
                },
                (totalCalories) => totalCalories.ToString());
    }
}