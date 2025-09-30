using System.Text.Json.Serialization;

namespace ProgressSyntha.Models;

/// <summary>
/// Retrieval results structure
/// </summary>
public class RetrievalResults
{
    [JsonPropertyName("resources")]
    public Dictionary<string, KnowledgeboxResource>? Resources { get; set; }

    [JsonPropertyName("relations")]
    public RetrievalRelations? Relations { get; set; }

    [JsonPropertyName("query")]
    public string? Query { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("page_number")]
    public int PageNumber { get; set; }
}

/// <summary>
/// Retrieval relations structure
/// </summary>
public class RetrievalRelations
{
    [JsonPropertyName("entities")]
    public Dictionary<string, object>? Entities { get; set; }
}

/// <summary>
/// Retrieval match structure
/// </summary>
public class RetrievalMatch
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
/// Answer object containing the generated response
/// </summary>
public class Answer
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("ids_paragraphs")]
    public string[] IdsParagraphs { get; set; } = Array.Empty<string>();

    [JsonPropertyName("paragraphs")]
    public string[]? Paragraphs { get; set; }
}
