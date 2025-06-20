using System.Text.Json.Serialization;

namespace ProgressSyntha.Models;

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
