using System.Text.Json.Serialization;

namespace ProgressSyntha.Models;

/// <summary>
/// Request for Find operations on a Knowledge Box
/// </summary>
public class FindRequest
{
    [JsonPropertyName("query")]
    public string Query { get; set; } = string.Empty;

    [JsonPropertyName("filter_expression")]
    public string? FilterExpression { get; set; }

    [JsonPropertyName("fields")]
    public string[] Fields { get; set; } = Array.Empty<string>();

    [JsonPropertyName("filters")]
    public string[] Filters { get; set; } = Array.Empty<string>();

    [JsonPropertyName("top_k")]
    public int? TopK { get; set; } = 20;

    [JsonPropertyName("min_score_semantic")]
    public double? MinScoreSemantic { get; set; }

    [JsonPropertyName("min_score_bm25")]
    public double? MinScoreBm25 { get; set; } = 0;

    [JsonPropertyName("vectorset")]
    public string? Vectorset { get; set; }

    [JsonPropertyName("range_creation_start")]
    public DateTime? RangeCreationStart { get; set; }

    [JsonPropertyName("range_creation_end")]
    public DateTime? RangeCreationEnd { get; set; }

    [JsonPropertyName("range_modification_start")]
    public DateTime? RangeModificationStart { get; set; }

    [JsonPropertyName("range_modification_end")]
    public DateTime? RangeModificationEnd { get; set; }

    [JsonPropertyName("features")]
    public SearchOption[] Features { get; set; } = new[] { SearchOption.Keyword, SearchOption.Semantic };

    [JsonPropertyName("debug")]
    public bool Debug { get; set; } = false;

    [JsonPropertyName("highlight")]
    public bool Highlight { get; set; } = false;

    [JsonPropertyName("show")]
    public ResourceProperties[] Show { get; set; } = new[] { ResourceProperties.Basic };

    [JsonPropertyName("field_type")]
    public FieldType[] FieldTypes { get; set; } = new[] { 
        FieldType.Text, 
        FieldType.File, 
        FieldType.Link, 
        FieldType.Conversation, 
        FieldType.Generic 
    };

    [JsonPropertyName("with_duplicates")]
    public bool WithDuplicates { get; set; } = false;

    [JsonPropertyName("with_synonyms")]
    public bool WithSynonyms { get; set; } = false;

    [JsonPropertyName("autofilter")]
    public bool Autofilter { get; set; } = false;

    [JsonPropertyName("security_groups")]
    public string[] SecurityGroups { get; set; } = Array.Empty<string>();

    [JsonPropertyName("show_hidden")]
    public bool ShowHidden { get; set; } = false;

    [JsonPropertyName("rank_fusion")]
    public RankFusionMethod RankFusion { get; set; } = RankFusionMethod.Rrf;

    [JsonPropertyName("reranker")]
    public RerankerMethod Reranker { get; set; } = RerankerMethod.Predict;
}

/// <summary>
/// Search option for Find operations
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<SearchOption>))]
public enum SearchOption
{
    [JsonPropertyName("keyword")]
    Keyword,
    
    [JsonPropertyName("fulltext")]
    Fulltext,
    
    [JsonPropertyName("semantic")]
    Semantic
}

/// <summary>
/// Field type for Find operations
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<FieldType>))]
public enum FieldType
{
    [JsonPropertyName("text")]
    Text,
    
    [JsonPropertyName("file")]
    File,
    
    [JsonPropertyName("link")]
    Link,
    
    [JsonPropertyName("conversation")]
    Conversation,
    
    [JsonPropertyName("generic")]
    Generic
}

/// <summary>
/// Rank fusion method for combining results
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<RankFusionMethod>))]
public enum RankFusionMethod
{
    [JsonPropertyName("rrf")]
    Rrf
}

/// <summary>
/// Reranker method for fine-tuning results
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<RerankerMethod>))]
public enum RerankerMethod
{
    [JsonPropertyName("predict")]
    Predict
}

/// <summary>
/// Score type for paragraph scoring
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<ScoreType>))]
public enum ScoreType
{
    [JsonPropertyName("VECTOR")]
    Vector,
    
    [JsonPropertyName("RERANKER")]
    Reranker
}

/// <summary>
/// Response for Find operations containing knowledge box search results with paragraph-level details
/// </summary>
public class KnowledgeboxFindResults
{
    [JsonPropertyName("resources")]
    public Dictionary<string, FindResource> Resources { get; set; } = new Dictionary<string, FindResource>();

    [JsonPropertyName("relations")]
    public FindRelationsData? Relations { get; set; }

    [JsonPropertyName("query")]
    public string? Query { get; set; }

    [JsonPropertyName("rephrased_query")]
    public string? RephrasedQuery { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("page_number")]
    public int PageNumber { get; set; }

    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }

    [JsonPropertyName("next_page")]
    public bool NextPage { get; set; }

    [JsonPropertyName("nodes")]
    public List<Dictionary<string, string>>? Nodes { get; set; }

    [JsonPropertyName("shards")]
    public string[]? Shards { get; set; }

    [JsonPropertyName("autofilters")]
    public object[]? Autofilters { get; set; }

    [JsonPropertyName("min_score")]
    public Dictionary<string, double>? MinScore { get; set; }

    [JsonPropertyName("best_matches")]
    public string[]? BestMatches { get; set; }

    [JsonPropertyName("metrics")]
    public Dictionary<string, object>? Metrics { get; set; }

    /// <summary>
    /// Helper property to get resources as an enumerable
    /// </summary>
    [JsonIgnore]
    public IEnumerable<FindResource> ResourceList => Resources.Values;
}

/// <summary>
/// Extended Knowledge box resource representation for find results
/// </summary>
public class FindResource
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("thumbnail")]
    public string? Thumbnail { get; set; }

    [JsonPropertyName("metadata")]
    public FindResourceMetadata? Metadata { get; set; }

    [JsonPropertyName("usermetadata")]
    public FindUserMetadata? UserMetadata { get; set; }

    [JsonPropertyName("fieldmetadata")]
    public FindFieldMetadataItem[]? FieldMetadata { get; set; }

    [JsonPropertyName("computedmetadata")]
    public FindComputedMetadata? ComputedMetadata { get; set; }

    [JsonPropertyName("created")]
    public DateTime? Created { get; set; }

    [JsonPropertyName("modified")]
    public DateTime? Modified { get; set; }

    [JsonPropertyName("last_seqid")]
    public int LastSeqId { get; set; }
    
    [JsonPropertyName("last_account_seq")]
    public int? LastAccountSeq { get; set; }
    
    [JsonPropertyName("queue")]
    public string? Queue { get; set; }
    
    [JsonPropertyName("hidden")]
    public bool Hidden { get; set; }
    
    [JsonPropertyName("origin")]
    public FindResourceOrigin? Origin { get; set; }
    
    [JsonPropertyName("extra")]
    public object? Extra { get; set; }
    
    [JsonPropertyName("relations")]
    public FindResourceRelation[]? Relations { get; set; }
    
    [JsonPropertyName("data")]
    public object? Data { get; set; }
    
    [JsonPropertyName("security")]
    public FindSecurityInfo? Security { get; set; }

    [JsonPropertyName("fields")]
    public Dictionary<string, FindFieldData>? Fields { get; set; }
}

/// <summary>
/// Field data containing paragraphs
/// </summary>
public class FindFieldData
{
    [JsonPropertyName("paragraphs")]
    public Dictionary<string, FindParagraphData> Paragraphs { get; set; } = new Dictionary<string, FindParagraphData>();
}

/// <summary>
/// Paragraph data with score and position information
/// </summary>
public class FindParagraphData
{
    [JsonPropertyName("score")]
    public double Score { get; set; }

    [JsonPropertyName("score_type")]
    public string? ScoreTypeString { get; set; }

    [JsonIgnore]
    public ScoreType ScoreType => 
        !string.IsNullOrEmpty(ScoreTypeString) && Enum.TryParse<ScoreType>(ScoreTypeString, out var type)
            ? type 
            : ScoreType.Vector;

    [JsonPropertyName("order")]
    public int Order { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("labels")]
    public string[] Labels { get; set; } = Array.Empty<string>();

    [JsonPropertyName("position")]
    public FindParagraphPosition? Position { get; set; }

    [JsonPropertyName("fuzzy_result")]
    public bool FuzzyResult { get; set; }

    [JsonPropertyName("page_with_visual")]
    public bool PageWithVisual { get; set; }

    [JsonPropertyName("reference")]
    public string? Reference { get; set; }

    [JsonPropertyName("is_a_table")]
    public bool IsTable { get; set; }

    [JsonPropertyName("relevant_relations")]
    public FindRelevantRelations? RelevantRelations { get; set; }
}

/// <summary>
/// Position information for a paragraph
/// </summary>
public class FindParagraphPosition
{
    [JsonPropertyName("page_number")]
    public int PageNumber { get; set; }

    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("start")]
    public int Start { get; set; }

    [JsonPropertyName("end")]
    public int End { get; set; }

    [JsonPropertyName("start_seconds")]
    public double[] StartSeconds { get; set; } = Array.Empty<double>();

    [JsonPropertyName("end_seconds")]
    public double[] EndSeconds { get; set; } = Array.Empty<double>();
}

/// <summary>
/// Relations relevant to a paragraph
/// </summary>
public class FindRelevantRelations
{
    [JsonPropertyName("entities")]
    public Dictionary<string, FindEntityRelations>? Entities { get; set; }
}

/// <summary>
/// Resource metadata
/// </summary>
public class FindResourceMetadata
{
    [JsonPropertyName("metadata")]
    public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("languages")]
    public string[] Languages { get; set; } = Array.Empty<string>();

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}

/// <summary>
/// User provided metadata for a resource
/// </summary>
public class FindUserMetadata
{
    [JsonPropertyName("classifications")]
    public FindClassificationItem[] Classifications { get; set; } = Array.Empty<FindClassificationItem>();

    [JsonPropertyName("relations")]
    public object[]? Relations { get; set; }
}

/// <summary>
/// Classification information
/// </summary>
public class FindClassificationItem
{
    [JsonPropertyName("labelset")]
    public string? LabelSet { get; set; }

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("cancelled_by_user")]
    public bool? CancelledByUser { get; set; }
}

/// <summary>
/// Field metadata item
/// </summary>
public class FindFieldMetadataItem
{
    [JsonPropertyName("paragraphs")]
    public object[] Paragraphs { get; set; } = Array.Empty<object>();

    [JsonPropertyName("question_answers")]
    public object[] QuestionAnswers { get; set; } = Array.Empty<object>();

    [JsonPropertyName("field")]
    public FindFieldInfo? Field { get; set; }
}

/// <summary>
/// Field information
/// </summary>
public class FindFieldInfo
{
    [JsonPropertyName("field_type")]
    public string? FieldType { get; set; }

    [JsonPropertyName("field")]
    public string? Field { get; set; }
}

/// <summary>
/// Computed metadata for a resource
/// </summary>
public class FindComputedMetadata
{
    [JsonPropertyName("field_classifications")]
    public FindFieldClassificationItem[] FieldClassifications { get; set; } = Array.Empty<FindFieldClassificationItem>();
}

/// <summary>
/// Field classification
/// </summary>
public class FindFieldClassificationItem
{
    [JsonPropertyName("field")]
    public FindFieldInfo? Field { get; set; }

    [JsonPropertyName("classifications")]
    public FindClassificationItem[] Classifications { get; set; } = Array.Empty<FindClassificationItem>();
}

/// <summary>
/// Resource origin information
/// </summary>
public class FindResourceOrigin
{
    [JsonPropertyName("source_id")]
    public string? SourceId { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("created")]
    public DateTime? Created { get; set; }

    [JsonPropertyName("modified")]
    public DateTime? Modified { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

    [JsonPropertyName("tags")]
    public string[] Tags { get; set; } = Array.Empty<string>();

    [JsonPropertyName("collaborators")]
    public string[] Collaborators { get; set; } = Array.Empty<string>();

    [JsonPropertyName("filename")]
    public string? Filename { get; set; }

    [JsonPropertyName("related")]
    public string[] Related { get; set; } = Array.Empty<string>();

    [JsonPropertyName("path")]
    public string? Path { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }
}

/// <summary>
/// Resource relation
/// </summary>
public class FindResourceRelation
{
    [JsonPropertyName("relation")]
    public string? Relation { get; set; }

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("metadata")]
    public FindRelationMetadata? Metadata { get; set; }

    [JsonPropertyName("from")]
    public FindEntityInfo? From { get; set; }

    [JsonPropertyName("to")]
    public FindEntityInfo? To { get; set; }
}

/// <summary>
/// Relation metadata
/// </summary>
public class FindRelationMetadata
{
    [JsonPropertyName("paragraph_id")]
    public string? ParagraphId { get; set; }

    [JsonPropertyName("source_start")]
    public int SourceStart { get; set; }

    [JsonPropertyName("source_end")]
    public int SourceEnd { get; set; }

    [JsonPropertyName("to_start")]
    public int ToStart { get; set; }

    [JsonPropertyName("to_end")]
    public int ToEnd { get; set; }

    [JsonPropertyName("data_augmentation_task_id")]
    public string? DataAugmentationTaskId { get; set; }
}

/// <summary>
/// Entity information
/// </summary>
public class FindEntityInfo
{
    [JsonPropertyName("value")]
    public string? Value { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("group")]
    public string? Group { get; set; }
}

/// <summary>
/// Security information
/// </summary>
public class FindSecurityInfo
{
    [JsonPropertyName("access_groups")]
    public string[] AccessGroups { get; set; } = Array.Empty<string>();
}

/// <summary>
/// Relations data structure for search results
/// </summary>
public class FindRelationsData
{
    [JsonPropertyName("entities")]
    public Dictionary<string, FindEntityRelations> Entities { get; set; } = new Dictionary<string, FindEntityRelations>();
}

/// <summary>
/// Entity relations for search results
/// </summary>
public class FindEntityRelations
{
    [JsonPropertyName("related_to")]
    public List<FindEntityRelation> RelatedTo { get; set; } = new List<FindEntityRelation>();
}

/// <summary>
/// Individual entity relation
/// </summary>
public class FindEntityRelation
{
    [JsonPropertyName("entity")]
    public string? Entity { get; set; }

    [JsonPropertyName("entity_type")]
    public string? EntityType { get; set; }

    [JsonPropertyName("entity_subtype")]
    public string? EntitySubtype { get; set; }

    [JsonPropertyName("relation")]
    public string? Relation { get; set; }

    [JsonPropertyName("relation_label")]
    public string? RelationLabel { get; set; }

    [JsonPropertyName("direction")]
    public string? Direction { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    [JsonPropertyName("resource_id")]
    public string? ResourceId { get; set; }
}