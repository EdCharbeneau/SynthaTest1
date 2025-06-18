using System.Text.Json.Serialization;

namespace ProgressSyntha.Models;

/// <summary>
/// Request payload for asking questions to a Knowledge Box
/// </summary>
public record AskRequest
{
    [JsonPropertyName("query")]
    public string Query { get; set; } = string.Empty;

    [JsonPropertyName("audit_metadata")]
    public Dictionary<string, string>? AuditMetadata { get; set; }

    [JsonPropertyName("top_k")]
    public int TopK { get; set; } = 20;

    [JsonPropertyName("filter_expression")]
    public object? FilterExpression { get; set; }

    [JsonPropertyName("fields")]
    public string[] Fields { get; set; } = Array.Empty<string>();

    [JsonPropertyName("filters")]
    public object[] Filters { get; set; } = Array.Empty<object>();

    [JsonPropertyName("keyword_filters")]
    public object[] KeywordFilters { get; set; } = Array.Empty<object>();

    [JsonPropertyName("vectorset")]
    public string? Vectorset { get; set; }

    [JsonPropertyName("min_score")]
    public double? MinScore { get; set; }

    [JsonPropertyName("show")]
    public string[] Show { get; set; } = new[] { "basic", "values", "origin" };

    [JsonPropertyName("features")]
    public string[] Features { get; set; } = new[] { "keyword", "semantic" };

    [JsonPropertyName("highlight")]
    public bool Highlight { get; set; } = false;

    [JsonPropertyName("citations")]
    public bool Citations { get; set; } = true;

    [JsonPropertyName("rephrase")]
    public bool Rephrase { get; set; } = true;

    [JsonPropertyName("debug")]
    public bool Debug { get; set; } = true;

    [JsonPropertyName("show_hidden")]
    public bool ShowHidden { get; set; } = false;

    [JsonPropertyName("reranker")]
    public string Reranker { get; set; } = "predict";

    [JsonPropertyName("autofilter")]
    public bool Autofilter { get; set; } = false;

    [JsonPropertyName("rag_strategies")]
    public RagStrategy[] RagStrategies { get; set; } = Array.Empty<RagStrategy>();

    [JsonPropertyName("context")]
    public object[] Context { get; set; } = Array.Empty<object>();
}

/// <summary>
/// Synchronous response for ask requests
/// </summary>
public class SyncAskResponse
{
    [JsonPropertyName("answer")]
    public string? Answer { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("retrieval_results")]
    public RetrievalResults? RetrievalResults { get; set; }

    [JsonPropertyName("retrieval_best_matches")]
    public RetrievalMatch[]? RetrievalBestMatches { get; set; }

    [JsonPropertyName("learning_id")]
    public string? LearningId { get; set; }

    [JsonPropertyName("citations")]
    public Dictionary<string, object>? Citations { get; set; }

    [JsonPropertyName("relations")]
    public object[]? Relations { get; set; }
}

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
