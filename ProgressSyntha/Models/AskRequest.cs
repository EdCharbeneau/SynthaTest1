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
    public bool Debug { get; set; } = false;

    [JsonPropertyName("show_hidden")]
    public bool ShowHidden { get; set; } = false;

    [JsonPropertyName("reranker")]
    public string Reranker { get; set; } = "predict";

    [JsonPropertyName("autofilter")]
    public bool Autofilter { get; set; } = false;

    [JsonPropertyName("rag_strategies")]
    public RagStrategy[] RagStrategies { get; set; } = Array.Empty<RagStrategy>();

    [JsonPropertyName("chat_history")]
    public ChatHistoryItem[] ChatHistory { get; set; } = Array.Empty<ChatHistoryItem>();
}

public record ChatHistoryItem
{
	[JsonPropertyName("author")]
	public string Author { get; set; }

	[JsonPropertyName("text")]
	public string Text { get; set; }
}
