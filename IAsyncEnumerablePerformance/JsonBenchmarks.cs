using BenchmarkDotNet.Attributes;

namespace IAsyncEnumerablePerformance;

[MemoryDiagnoser]
public class JsonBenchmarks
{
    private readonly string folder = Path.Combine(Directory.GetCurrentDirectory(), "json");

    [Benchmark]
    public async Task<Dictionary<DateOnly, int>> AggregateFormListAsync()
    {
        return await Aggregator.AggregateFormListAsync(folder);
    }

    [Benchmark]
    public async Task<Dictionary<DateOnly, int>> AggregateFormListWithLinqAsync()
    {
        return await Aggregator.AggregateFormListWithLinqAsync(folder);
    }

    [Benchmark]
    public async Task<Dictionary<DateOnly, int>> AggregateFromTaskListAsync()
    {
        return await Aggregator.AggregateFromTaskListAsync(folder);
    }

    [Benchmark]
    public async Task<Dictionary<DateOnly, int>> AggregateFromAsyncEnumerableAsync()
    {
        return await Aggregator.AggregateFromAsyncEnumerableAsync(folder);
    }

    [Benchmark]
    public async Task<Dictionary<DateOnly, int>> AggregateFromAsyncEnumerableWithLinqAsync()
    {
        return await Aggregator.AggregateFromAsyncEnumerableWithLinqAsync(folder);
    }
}
