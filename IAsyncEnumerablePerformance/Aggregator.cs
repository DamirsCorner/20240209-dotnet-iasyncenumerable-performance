namespace IAsyncEnumerablePerformance;

public class Aggregator
{
    public static async Task<Dictionary<DateOnly, int>> AggregateFormListAsync(string folder)
    {
        var aggregates = new Dictionary<DateOnly, int>();
        var items = await JsonLoader.LoadAsListAsync(folder);
        foreach (var item in items)
        {
            var date = DateOnly.FromDateTime(item.DateTime);
            if (!aggregates.TryGetValue(date, out var aggregate))
            {
                aggregate = 0;
            }
            aggregates[date] = aggregate + item.Value;
        }
        return aggregates;
    }

    public static async Task<Dictionary<DateOnly, int>> AggregateFormListWithLinqAsync(
        string folder
    )
    {
        var items = await JsonLoader.LoadAsListAsync(folder);
        return items
            .GroupBy(item => DateOnly.FromDateTime(item.DateTime))
            .ToDictionary(group => group.Key, group => group.Sum(item => item.Value));
    }

    public static async Task<Dictionary<DateOnly, int>> AggregateFromTaskListAsync(string folder)
    {
        var aggregates = new Dictionary<DateOnly, int>();
        var tasks = JsonLoader.LoadAsTaskList(folder);
        foreach (var task in tasks)
        {
            var items = await task;
            if (items != null)
            {
                foreach (var item in items)
                {
                    var date = DateOnly.FromDateTime(item.DateTime);
                    if (!aggregates.TryGetValue(date, out var aggregate))
                    {
                        aggregate = 0;
                    }
                    aggregates[date] = aggregate + item.Value;
                }
            }
        }
        return aggregates;
    }

    public static async Task<Dictionary<DateOnly, int>> AggregateFromAsyncEnumerableAsync(
        string folder
    )
    {
        var aggregates = new Dictionary<DateOnly, int>();
        var items = JsonLoader.LoadAsAsyncEnumerable(folder);
        await foreach (var item in items)
        {
            var date = DateOnly.FromDateTime(item.DateTime);
            if (!aggregates.TryGetValue(date, out var aggregate))
            {
                aggregate = 0;
            }
            aggregates[date] = aggregate + item.Value;
        }
        return aggregates;
    }

    public static async Task<Dictionary<DateOnly, int>> AggregateFromAsyncEnumerableWithLinqAsync(
        string folder
    )
    {
        var items = JsonLoader.LoadAsAsyncEnumerable(folder);
        return await items
            .GroupBy(item => DateOnly.FromDateTime(item.DateTime))
            .ToDictionaryAsync(
                group => group.Key,
                group => group.SumAsync(item => item.Value).AsTask().Result
            );
    }
}
