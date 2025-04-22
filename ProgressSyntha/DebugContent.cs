using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProgressSyntha;

public class DebugContent : RAGContent
{
	[JsonPropertyName("type")]
	public string Type { get; set; } = "debug";

	[JsonPropertyName("metadata")]
	public DebugMetadata Metadata { get; set; } = new DebugMetadata();
}

public class DebugMetadata
{
	[JsonPropertyName("prompt_context")]
	public List<string> PromptContext { get; set; } = new();

	[JsonPropertyName("predict_request")]
	public PredictRequest PredictRequest { get; set; } = new PredictRequest();
}

public class PredictRequest
{
	[JsonPropertyName("question")]
	public string Question { get; set; }

	[JsonPropertyName("user_id")]
	public string UserId { get; set; }

	[JsonPropertyName("retrieval")]
	public bool Retrieval { get; set; }

	[JsonPropertyName("system")]
	public string System { get; set; }

	[JsonPropertyName("query_context")]
	public Dictionary<string, string> QueryContext { get; set; } = new();

	[JsonPropertyName("query_context_order")]
	public Dictionary<string, int> QueryContextOrder { get; set; } = new();

	[JsonPropertyName("chat_history")]
	public List<string> ChatHistory { get; set; } = new();

	[JsonPropertyName("truncate")]
	public bool Truncate { get; set; }

	[JsonPropertyName("user_prompt")]
	public string UserPrompt { get; set; }

	[JsonPropertyName("citations")]
	public bool Citations { get; set; }

	[JsonPropertyName("citation_threshold")]
	public string CitationThreshold { get; set; }

	[JsonPropertyName("generative_model")]
	public string GenerativeModel { get; set; }

	[JsonPropertyName("max_tokens")]
	public int? MaxTokens { get; set; }

	[JsonPropertyName("query_context_images")]
	public Dictionary<string, string> QueryContextImages { get; set; } = new();

	[JsonPropertyName("prefer_markdown")]
	public bool PreferMarkdown { get; set; }

	[JsonPropertyName("json_schema")]
	public string JsonSchema { get; set; }

	[JsonPropertyName("rerank_context")]
	public bool RerankContext { get; set; }

	[JsonPropertyName("top_k")]
	public int TopK { get; set; }

	[JsonPropertyName("format_prompt")]
	public bool FormatPrompt { get; set; }
}
