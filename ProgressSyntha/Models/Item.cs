namespace ProgressSyntha.Models;

using ProgressSyntha.Models.Streaming;
using System.Text.Json.Serialization;

public class Item
{
	public RAGContent RAGContent { get; set; }
}






public class FieldClassification
{
	public Field? Field { get; set; } 
	public List<Classification> Classifications { get; set; } = new();
}

public record Field(string? FieldType, string? FieldName);

public record Classification(string? LabelSet, string? Label);



public record TextValueWrapper([property: JsonPropertyName("value")] TextValue? Item);

public record TextValue(string? Body, string? Format, string? Md5, string? ExtractStrategy);
