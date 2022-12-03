using AOC2022.Domain;
using AOC2022.Domain.Common;

namespace AOC2022.Infrastructure.InputRetrievers;

public class SampleInputRetriever : IInputRetriever
{
    private readonly IReadOnlyDictionary<ValidDayNumber, string[]> _sampleInputs = new Dictionary<ValidDayNumber, string[]>
    {
        [new ValidDayNumber(1)] = new[]
        {
            "1000",
            "2000",
            "3000",
            "",
            "4000",
            "",
            "5000",
            "6000",
            "",
            "7000",
            "8000",
            "9000",
            "",
            "10000"
        },
        [new ValidDayNumber(2)] = new[]
        {
            "A Y",
            "B X",
            "C Z"
        },
        [new ValidDayNumber(3)] = new[]
        {
            "vJrwpWtwJgWrhcsFMMfFFhFp",
            "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
            "PmmdzqPrVvPwwTWBwg",
            "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
            "ttgJtRGJQctTZtZT",
            "CrZsJsPPZsGzwwsLwLmpwMDw"
        }
    };


    public IAsyncEnumerable<string> GetInputForDay(ValidDayNumber day)
    {
        return _sampleInputs[day].ToAsyncEnumerable();
    }
}