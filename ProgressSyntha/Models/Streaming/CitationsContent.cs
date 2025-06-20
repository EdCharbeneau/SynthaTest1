using System.Text.Json.Serialization;

namespace ProgressSyntha.Models.Streaming;

public class CitationsContent : RAGContent
{
    [JsonPropertyName("citations")]
    public Dictionary<string, int[][]> Citations { get; set; } = new();
}

public class CitationItem
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("citations")]
    public Dictionary<string, int[][]> Citations { get; set; } = new();
}
