using System.Text.Json.Serialization;

namespace ProgressSyntha.Models;

/// <summary>
/// Represents a Knowledge Box object
/// </summary>
public class KnowledgeBoxObj
{
    [JsonPropertyName("uuid")]
    public string? Uuid { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("learning_configuration")]
    public LearningConfiguration? LearningConfiguration { get; set; }

    [JsonPropertyName("release_channel")]
    public string? ReleaseChannel { get; set; }
}

/// <summary>
/// Learning configuration for a Knowledge Box
/// </summary>
public class LearningConfiguration
{
    [JsonPropertyName("anonymous_telemetry")]
    public bool? AnonymousTelemetry { get; set; }
}
