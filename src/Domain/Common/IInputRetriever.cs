namespace AOC2022.Domain.Common;

public interface IInputRetriever
{
    public IAsyncEnumerable<string> GetInputForDay(ValidDayNumber day);
}