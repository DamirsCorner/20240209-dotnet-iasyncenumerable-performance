using System.Text.Json.Serialization;

namespace IAsyncEnumerablePerformance;

public class TimeValue
{
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime DateTime { get; set; }

    [JsonConverter(typeof(Int32FromStringConverter))]
    public int Value { get; set; }
}
