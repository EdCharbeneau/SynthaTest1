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

    [JsonPropertyName("config")]
    public KnowledgeBoxConfig? Config { get; set; }

    [JsonPropertyName("model")]
    public KnowledgeBoxModel? Model { get; set; }

    /// <summary>
    /// Gets the title from the config object for backward compatibility
    /// </summary>
    [JsonIgnore]
    public string? Title => Config?.Title;

    /// <summary>
    /// Gets the description from the config object for backward compatibility
    /// </summary>
    [JsonIgnore]
    public string? Description => Config?.Description;
}

/// <summary>
/// Configuration for a Knowledge Box
/// </summary>
public class KnowledgeBoxConfig
{
    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("learning_configuration")]
    public LearningConfiguration? LearningConfiguration { get; set; }

    [JsonPropertyName("external_index_provider")]
    public ExternalIndexProvider? ExternalIndexProvider { get; set; }

    [JsonPropertyName("configured_external_index_provider")]
    public object? ConfiguredExternalIndexProvider { get; set; }

    [JsonPropertyName("similarity")]
    public string? Similarity { get; set; }

    [JsonPropertyName("hidden_resources_enabled")]
    public bool HiddenResourcesEnabled { get; set; }

    [JsonPropertyName("hidden_resources_hide_on_creation")]
    public bool HiddenResourcesHideOnCreation { get; set; }

    [JsonPropertyName("release_channel")]
    public string? ReleaseChannel { get; set; }
}

/// <summary>
/// External index provider configuration
/// </summary>
public class ExternalIndexProvider
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("api_key")]
    public string? ApiKey { get; set; }

    [JsonPropertyName("serverless_cloud")]
    public string? ServerlessCloud { get; set; }
}

/// <summary>
/// Model configuration for a Knowledge Box
/// </summary>
public class KnowledgeBoxModel
{
    [JsonPropertyName("similarity_function")]
    public string? SimilarityFunction { get; set; }

    [JsonPropertyName("vector_dimension")]
    public int VectorDimension { get; set; }

    [JsonPropertyName("default_min_score")]
    public double DefaultMinScore { get; set; }
}

/// <summary>
/// Learning configuration for a Knowledge Box
/// </summary>
public class LearningConfiguration
{
    [JsonPropertyName("anonymous_telemetry")]
    public bool? AnonymousTelemetry { get; set; }
}
