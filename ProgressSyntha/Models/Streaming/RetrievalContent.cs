using System.Text.Json.Serialization;

namespace ProgressSyntha.Models.Streaming;

public class RetrievalContent : RAGContent
{
    [JsonPropertyName("results")]
    public Results? Results { get; set; }

    [JsonPropertyName("best_matches")]
    public List<BestMatch>? BestMatches { get; set; }
}

public class Results
{
    [JsonPropertyName("resources")]
    public Dictionary<string, Resource> Resources { get; set; } = new();

    [JsonPropertyName("relations")]
    public Relations? Relations { get; set; }

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

    [JsonPropertyName("shards")]
    public List<string>? Shards { get; set; }

    [JsonPropertyName("autofilters")]
    public List<object>? AutoFilters { get; set; }

    [JsonPropertyName("min_score")]
    public MinScore? MinScore { get; set; }

    [JsonPropertyName("best_matches")]
    public List<string>? BestMatches { get; set; }
}

public class Relations
{
    [JsonPropertyName("entities")]
    public Dictionary<string, object> Entities { get; set; } = new();
}

public class MinScore
{
    [JsonPropertyName("semantic")]
    public double Semantic { get; set; }

    [JsonPropertyName("bm25")]
    public double Bm25 { get; set; }
}

public class BestMatch
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

public record Resource(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("slug")] string? Slug,
    [property: JsonPropertyName("title")] string? Title,
    [property: JsonPropertyName("summary")] string? Summary,
    [property: JsonPropertyName("icon")] string? Icon,
    [property: JsonPropertyName("thumbnail")] string? Thumbnail,
    [property: JsonPropertyName("created")] string Created,
    [property: JsonPropertyName("last_seqid")] int LastSeqId,
    [property: JsonPropertyName("queue")] string? Queue,
    [property: JsonPropertyName("hidden")] bool Hidden)
{
    [JsonPropertyName("metadata")]
    public Metadata Metadata { get; set; } = new();

    [JsonPropertyName("usermetadata")]
    public UserMetadata? UserMetadata { get; set; }

    [JsonPropertyName("fieldmetadata")]
    public List<object>? FieldMetadata { get; set; }

    [JsonPropertyName("computedmetadata")]
    public ComputedMetadata ComputedMetadata { get; set; } = new();

    [JsonPropertyName("data")]
    public Data Data { get; set; } = new();

    [JsonPropertyName("fields")]
    public Dictionary<string, FieldContainer>? Fields { get; set; }

    [JsonPropertyName("modified")]
    public string? Modified { get; set; }

    [JsonPropertyName("origin")]
    public Origin? Origin { get; set; }
}

public class Origin
{
    [JsonPropertyName("source_id")]
    public string? SourceId { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object> Metadata { get; set; } = new();

    [JsonPropertyName("tags")]
    public List<object> Tags { get; set; } = new();

    [JsonPropertyName("collaborators")]
    public List<object> Collaborators { get; set; } = new();

    [JsonPropertyName("filename")]
    public string? Filename { get; set; }

    [JsonPropertyName("related")]
    public List<object> Related { get; set; } = new();

    [JsonPropertyName("path")]
    public string? Path { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }
}

public class Metadata
{
    [JsonPropertyName("metadata")]
    public Dictionary<string, object> MetadataDetails { get; set; } = new();

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("languages")]
    public List<string> Languages { get; set; } = new();

    [JsonPropertyName("status")]
    public string? Status { get; set; }
}

public class UserMetadata
{
    [JsonPropertyName("classifications")]
    public List<Classification> Classifications { get; set; } = new();

    [JsonPropertyName("relations")]
    public List<object> Relations { get; set; } = new();
}

public class Classification
{
    [JsonPropertyName("labelset")]
    public string? Labelset { get; set; }

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("cancelled_by_user")]
    public bool? CancelledByUser { get; set; }
}

public class ComputedMetadata
{
    [JsonPropertyName("field_classifications")]
    public List<FieldClassification> FieldClassifications { get; set; } = new();
}

public class FieldClassification
{
    [JsonPropertyName("field")]
    public Field? Field { get; set; }

    [JsonPropertyName("classifications")]
    public List<Classification> Classifications { get; set; } = new();
}

public class Field
{
    [JsonPropertyName("field_type")]
    public string? FieldType { get; set; }

    [JsonPropertyName("field")]
    public string? FieldName { get; set; }
}

public class Data
{
    [JsonPropertyName("texts")]
    public Dictionary<string, TextValueWrapper> Texts { get; set; } = new();

    [JsonPropertyName("files")]
    public Dictionary<string, FileValueWrapper>? Files { get; set; }

    [JsonPropertyName("links")]
    public Dictionary<string, LinkValueWrapper>? Links { get; set; }

    [JsonPropertyName("generics")]
    public Dictionary<string, GenericValueWrapper>? Generics { get; set; }
}

public class TextValueWrapper
{
    [JsonPropertyName("value")]
    public TextValue? Value { get; set; }
}

public class TextValue
{
    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("format")]
    public string? Format { get; set; }

    [JsonPropertyName("md5")]
    public string? Md5 { get; set; }

    [JsonPropertyName("extract_strategy")]
    public string? ExtractStrategy { get; set; }
}

public class FileValueWrapper
{
    [JsonPropertyName("value")]
    public FileValue? Value { get; set; }
}

public class FileValue
{
    [JsonPropertyName("added")]
    public string? Added { get; set; }

    [JsonPropertyName("file")]
    public FileDetails? File { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("external")]
    public bool External { get; set; }

    [JsonPropertyName("extract_strategy")]
    public string? ExtractStrategy { get; set; }
}

public class FileDetails
{
    [JsonPropertyName("uri")]
    public string? Uri { get; set; }

    [JsonPropertyName("size")]
    public long Size { get; set; }

    [JsonPropertyName("content_type")]
    public string? ContentType { get; set; }

    [JsonPropertyName("filename")]
    public string? Filename { get; set; }

    [JsonPropertyName("md5")]
    public string? Md5 { get; set; }
}

public class LinkValueWrapper
{
    [JsonPropertyName("value")]
    public LinkValue? Value { get; set; }
}

public class LinkValue
{
    [JsonPropertyName("added")]
    public string? Added { get; set; }

    [JsonPropertyName("headers")]
    public Dictionary<string, object> Headers { get; set; } = new();

    [JsonPropertyName("cookies")]
    public Dictionary<string, object> Cookies { get; set; } = new();

    [JsonPropertyName("uri")]
    public string? Uri { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("localstorage")]
    public Dictionary<string, object> LocalStorage { get; set; } = new();

    [JsonPropertyName("css_selector")]
    public string? CssSelector { get; set; }

    [JsonPropertyName("xpath")]
    public string? XPath { get; set; }

    [JsonPropertyName("extract_strategy")]
    public string? ExtractStrategy { get; set; }
}

public class GenericValueWrapper
{
    [JsonPropertyName("value")]
    public string? Value { get; set; }
}

public class FieldContainer
{
    [JsonPropertyName("paragraphs")]
    public Dictionary<string, Paragraph> Paragraphs { get; set; } = new();
}

public class Paragraph
{
    [JsonPropertyName("score")]
    public double Score { get; set; }

    [JsonPropertyName("score_type")]
    public string? ScoreType { get; set; }

    [JsonPropertyName("order")]
    public int Order { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("labels")]
    public List<string>? Labels { get; set; }

    [JsonPropertyName("position")]
    public ParagraphPosition? Position { get; set; }

    [JsonPropertyName("fuzzy_result")]
    public bool FuzzyResult { get; set; }

    [JsonPropertyName("page_with_visual")]
    public bool PageWithVisual { get; set; }

    [JsonPropertyName("reference")]
    public string? Reference { get; set; }

    [JsonPropertyName("is_a_table")]
    public bool IsATable { get; set; }
}

public class ParagraphPosition
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
    public List<object> StartSeconds { get; set; } = new();

    [JsonPropertyName("end_seconds")]
    public List<object> EndSeconds { get; set; } = new();
}