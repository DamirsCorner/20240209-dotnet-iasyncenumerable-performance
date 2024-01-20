using System.Text.Json;

namespace IAsyncEnumerablePerformance;

public class JsonLoader
{
    private static readonly JsonSerializerOptions jsonSerializerOptions =
        new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, };

    public static async Task<List<TimeValue>> LoadAsListAsync(string folder)
    {
        var files = Directory.EnumerateFiles(folder, "*.json");
        var allItems = new List<TimeValue>();
        foreach (var file in files)
        {
            using var stream = File.OpenRead(file);
            var jsonItems = await JsonSerializer.DeserializeAsync<List<TimeValue>>(
                stream,
                jsonSerializerOptions
            );
            if (jsonItems != null)
            {
                allItems.AddRange(jsonItems);
            }
        }

        return allItems;
    }

    public static IEnumerable<Task<List<TimeValue>?>> LoadAsTaskList(string folder)
    {
        var files = Directory.EnumerateFiles(folder, "*.json");
        foreach (var file in files)
        {
            using var stream = File.OpenRead(file);
            yield return JsonSerializer
                .DeserializeAsync<List<TimeValue>>(stream, jsonSerializerOptions)
                .AsTask();
        }
    }

    public static async IAsyncEnumerable<TimeValue> LoadAsAsyncEnumerable(string folder)
    {
        var files = Directory.EnumerateFiles(folder, "*.json");
        foreach (var file in files)
        {
            using var stream = File.OpenRead(file);
            var jsonItems = JsonSerializer.DeserializeAsyncEnumerable<TimeValue>(
                stream,
                jsonSerializerOptions
            );
            await foreach (var jsonItem in jsonItems)
            {
                if (jsonItem != null)
                {
                    yield return jsonItem;
                }
            }
        }
    }
}
