using System.Text.Json.Serialization;

namespace ProgressSyntha.Models;

/// <summary>
/// Request for catalog search/listing resources
/// </summary>
public class CatalogRequest
{
    [JsonPropertyName("query")]
    public string Query { get; set; } = string.Empty;

    [JsonPropertyName("filter_expression")]
    public string? FilterExpression { get; set; }

    [JsonPropertyName("filters")]
    public string[] Filters { get; set; } = Array.Empty<string>();

    [JsonPropertyName("faceted")]
    public string[] Faceted { get; set; } = Array.Empty<string>();

    [JsonPropertyName("sort_field")]
    public SortField? SortField { get; set; }

    [JsonPropertyName("sort_limit")]
    public int? SortLimit { get; set; }

    [JsonPropertyName("sort_order")]
    public SortOrder SortOrder { get; set; } = SortOrder.Desc;

    [JsonPropertyName("page_number")]
    public int PageNumber { get; set; } = 0;

    [JsonPropertyName("page_size")]
    public int PageSize { get; set; } = 20;

    [JsonPropertyName("with_status")]
    public ResourceProcessingStatus? WithStatus { get; set; }

    [JsonPropertyName("range_creation_start")]
    public DateTime? RangeCreationStart { get; set; }

    [JsonPropertyName("range_creation_end")]
    public DateTime? RangeCreationEnd { get; set; }

    [JsonPropertyName("range_modification_start")]
    public DateTime? RangeModificationStart { get; set; }

    [JsonPropertyName("range_modification_end")]
    public DateTime? RangeModificationEnd { get; set; }

    [JsonPropertyName("hidden")]
    public bool? Hidden { get; set; }

    [JsonPropertyName("show")]
    public ResourceProperties[] Show { get; set; } = new[] { ResourceProperties.Basic, ResourceProperties.Errors };
}

/// <summary>
/// Response for catalog/search operations containing knowledge box search results
/// </summary>
public class KnowledgeboxSearchResults
{
    [JsonPropertyName("resources")]
    public Dictionary<string, KnowledgeboxResource> Resources { get; set; } = new Dictionary<string, KnowledgeboxResource>();

    [JsonPropertyName("facets")]
    public Dictionary<string, object>? Facets { get; set; }

    [JsonPropertyName("page_number")]
    public int PageNumber { get; set; }

    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("query")]
    public string? Query { get; set; }

    /// <summary>
    /// Helper property to get resources as an enumerable
    /// </summary>
    [JsonIgnore]
    public IEnumerable<KnowledgeboxResource> ResourceList => Resources.Values;
}

/// <summary>
/// Knowledge box resource representation
/// </summary>
public class KnowledgeboxResource
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("created")]
    public DateTime? Created { get; set; }

    [JsonPropertyName("modified")]
    public DateTime? Modified { get; set; }

    [JsonPropertyName("status")]
    public ResourceProcessingStatus? Status { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    [JsonPropertyName("fields")]
    public Dictionary<string, object>? Fields { get; set; }
}

/// <summary>
/// Sort field options for catalog queries
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<SortField>))]
public enum SortField
{
    [JsonPropertyName("created")]
    Created,
    [JsonPropertyName("modified")]
    Modified,
    [JsonPropertyName("title")]
    Title
}

/// <summary>
/// Sort order options
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<SortOrder>))]
public enum SortOrder
{
    [JsonPropertyName("asc")]
    Asc,
    [JsonPropertyName("desc")]
    Desc
}

/// <summary>
/// Resource processing status
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<ResourceProcessingStatus>))]
public enum ResourceProcessingStatus
{
    [JsonPropertyName("processed")]
    Processed,
    [JsonPropertyName("pending")]
    Pending,
    [JsonPropertyName("error")]
    Error
}

/// <summary>
/// Resource properties to include in responses
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<ResourceProperties>))]
public enum ResourceProperties
{
    [JsonPropertyName("basic")]
    Basic,
    [JsonPropertyName("origin")]
    Origin,
    [JsonPropertyName("relations")]
    Relations,
    [JsonPropertyName("values")]
    Values,
    [JsonPropertyName("extra")]
    Extra,
    [JsonPropertyName("extracted")]
    Extracted,
    [JsonPropertyName("errors")]
    Errors,
    [JsonPropertyName("fields")]
    Fields
}
