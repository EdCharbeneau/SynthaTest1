using System.Text.Json.Serialization;

namespace ProgressSyntha.Models;

/// <summary>
/// RAG strategy configuration
/// </summary>
public class RagStrategy
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("before")]
    public int Before { get; set; }

    [JsonPropertyName("after")]
    public int After { get; set; }
}

/// <summary>
/// Stream response item for real-time ask responses
/// </summary>
public class StreamResponse
{
	public RAGContent Item { get; set; }
}

/// <summary>
/// Retrieval information from search results
/// </summary>
public class Retrieval
{
    [JsonPropertyName("results")]
    public RetrievalResult[]? Results { get; set; }

    [JsonPropertyName("query")]
    public string? Query { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }
}

/// <summary>
/// Individual retrieval result
/// </summary>
public class RetrievalResult
{
    [JsonPropertyName("score")]
    public double Score { get; set; }

    [JsonPropertyName("field")]
    public RetrievalField? Field { get; set; }

    [JsonPropertyName("paragraph")]
    public string? Paragraph { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("rid")]
    public string? Rid { get; set; }
}

/// <summary>
/// Field information in retrieval results
/// </summary>
public class RetrievalField
{
    [JsonPropertyName("field")]
    public string? Field { get; set; }

    [JsonPropertyName("field_type")]
    public string? FieldType { get; set; }
}
