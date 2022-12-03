using System.Collections.Immutable;
using System.Diagnostics;

using AOC2022.Domain;
using AOC2022.Domain.Common;

namespace AOC2022.ConsoleRunner.Days;

public class Day03 : AdventOfCodeDay
{
    private readonly IInputRetriever _inputRetriever;

    public override required ValidDayNumber Number { get; init; }

    public Day03(IInputRetriever inputRetriever)
    {
        _inputRetriever = inputRetriever;
        Number = new ValidDayNumber(3);
    }
    protected override async Task<string> SolveTaskOne()
    {
        return await _inputRetriever
            .GetInputForDay(Number)
            .AggregateAsync(
                0,
                (sumOfPriorities, currentInput) =>
                {
                    var firstCompartment = currentInput[..(currentInput.Length / 2)];
                    var uniqueItemsInFirstCompartment = ImmutableHashSet
                        .CreateRange(firstCompartment);
                    var secondCompartment = currentInput.Substring(currentInput.Length / 2, currentInput.Length / 2);
                    var uniqueItemsInSecondCompartment = ImmutableHashSet
                        .CreateRange(secondCompartment);

                    var commonItems = uniqueItemsInFirstCompartment
                        .Intersect(uniqueItemsInSecondCompartment);

                    return commonItems.Aggregate(
                        sumOfPriorities,
                        (sumOfPriorities, currentDuplicateItem) =>
                        {
                            var priorityValue = CalculatePriority(currentDuplicateItem);
                            return sumOfPriorities + priorityValue;
                        });
                },
                acc => acc.ToString());
    }

    private static int CalculatePriority(char c) => c switch
    {
        >= 'a' and <= 'z' => c - 'a' + 1,
        >= 'A' and <= 'Z' => c - 'A' + 27,
        _ => throw new UnreachableException("Invalid input.")
    };

    protected override async Task<string> SolveTaskTwo()
    {
        return await _inputRetriever
            .GetInputForDay(Number)
            .AggregateAsync(
                (
                    prioritySum: 0,
                    currentTriplet: new Stack<ImmutableHashSet<char>>(3)
                ),
                (accumulator, input) =>
                {
                    accumulator.currentTriplet.Push(ImmutableHashSet.CreateRange(input));
                    if (accumulator.currentTriplet.Count == 3)
                    {
                        var elfOne = accumulator.currentTriplet.Pop();
                        var elfTwo = accumulator.currentTriplet.Pop();
                        var elfThree = accumulator.currentTriplet.Pop();

                        var common = elfOne.Intersect(elfTwo.Intersect(elfThree));

                        accumulator.currentTriplet = new Stack<ImmutableHashSet<char>>(3);

                        accumulator.prioritySum += CalculatePriority(common.Single());
                    }

                    return accumulator;
                },
                acc => acc.prioritySum.ToString());
    }
}