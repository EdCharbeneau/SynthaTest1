using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProgressSyntha;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(AnswerContent), typeDiscriminator: "answer")]
[JsonDerivedType(typeof(RetrievalContent), typeDiscriminator: "retrieval")]
[JsonDerivedType(typeof(CitationsContent), typeDiscriminator: "citations")]
[JsonDerivedType(typeof(StatusContent), typeDiscriminator: "status")]
[JsonDerivedType(typeof(MetaDataContent), typeDiscriminator: "metadata")]
[JsonDerivedType(typeof(DebugContent), typeDiscriminator: "debug")]

public class RAGContent {
	/// <summary>Gets or sets the raw representation of the content from an underlying implementation.</summary>
	/// <remarks>
	/// If an <see cref="AIContent"/> is created to represent some underlying object from another object
	/// model, this property can be used to store that original object. This can be useful for debugging or
	/// for enabling a consumer to access the underlying object model if needed.
	/// </remarks>
	[JsonIgnore]
	public object? RawRepresentation { get; set; }

	public string? Type { get; set; }

}

public class AnswerContent : RAGContent
{
	public string? Text { get; set; }
}

public class RetrievalContent : RAGContent
{
	public Results Results { get; set; }
}

public class StatusContent : RAGContent
{
	public string? Status { get; set; }
	public int Code { get; set; }
}

public class CitationsContent : RAGContent
{
	public Dictionary<string, object[]> Citations { get; set; } = new();
}

public class MetaDataContent : RAGContent
{
	public Tokens? Tokens { get; set; }
	public Timings? Timings { get; set; }
}

public class Tokens
{
	public int Input { get; set; }
	public int Output { get; set; }
	public double InputNuclia { get; set; }
	public double OutputNuclia { get; set; }
}

public class Timings
{
	public double GenerativeFirstChunk { get; set; }
	public double GenerativeTotal { get; set; }
}
