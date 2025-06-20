namespace ProgressSyntha.Models.Streaming;

public class RetrievalContent : RAGContent
{
	public Results Results { get; set; }
}

public class Results
{
	public Dictionary<string, Resource> Resources { get; set; } = new();
}

public record Resource(string Id,
					   string? Slug,
					   string? Title,
					   string? Summary,
					   string? Icon,
					   string? Thumbnail,
					   string Created,
					   int LastSeqId,
					   string? Queue,
					   bool Hidden)
{
	public Metadata Metadata { get; set; } = new();
	//public UserMetadata UserMetadata { get; set; }
	//public List<object> FieldMetadata { get; set; }
	public ComputedMetadata ComputedMetadata { get; set; } = new();
	public Data Data { get; set; } = new();
}

public class Metadata
{
	public Dictionary<string, object> MetadataDetails { get; set; } = new();
	public string? Language { get; set; }
	public List<string> Languages { get; set; } = new();
	public string? Status { get; set; }
}

public class UserMetadata
{
	public List<string> Classifications { get; set; } = new();
	public List<object> Relations { get; set; } = new();
}

public class ComputedMetadata
{
	public List<FieldClassification> FieldClassifications { get; set; } = new();
}

public class Data
{
	public Dictionary<string, TextValueWrapper> Texts { get; set; } = new();
}