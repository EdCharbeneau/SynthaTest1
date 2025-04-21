using System.Text.Json.Serialization;

public class Item
{
	public string Type { get; set; } // "answer" or "retrieval"
	public string Text { get; set; }
	public Results Results { get; set; }
}

public class Results
{
	public Dictionary<string, Resource> Resources { get; set; }
}

public class Resource
{
	public string Id { get; set; }
	public string Slug { get; set; }
	public string Title { get; set; }
	public string Summary { get; set; }
	public string Icon { get; set; }
	public string Thumbnail { get; set; }
	public Metadata Metadata { get; set; }
	//public UserMetadata UserMetadata { get; set; }
	//public List<object> FieldMetadata { get; set; }
	public ComputedMetadata ComputedMetadata { get; set; }
	public string Created { get; set; }
	public int LastSeqId { get; set; }
	public string Queue { get; set; }
	public bool Hidden { get; set; }
	public Data Data { get; set; }
}

public class Metadata
{
	public Dictionary<string, object> MetadataDetails { get; set; }
	public string Language { get; set; }
	public List<string> Languages { get; set; }
	public string Status { get; set; }
}

public class UserMetadata
{
	public List<string> Classifications { get; set; }
	public List<object> Relations { get; set; }
}

public class ComputedMetadata
{
	public List<FieldClassification> FieldClassifications { get; set; }
}

public class FieldClassification
{
	public Field Field { get; set; }
	public List<Classification> Classifications { get; set; }
}

public class Field
{
	public string FieldType { get; set; }
	public string FieldName { get; set; }
}

public class Classification
{
	public string LabelSet { get; set; }
	public string Label { get; set; }
}
public class Data
{
	public Dictionary<string, TextValueWrapper> Texts { get; set; }
}

public class TextValueWrapper
{
	[JsonPropertyName("value")]
	public TextValue Item { get; set; }
}

public class TextValue
{
	public string Body { get; set; }
	public string Format { get; set; }
	public string Md5 { get; set; }
	public string ExtractStrategy { get; set; }
}
