using System.Text.Json.Serialization;

namespace ProgressSyntha.Models.Streaming;

// When Syntha responds with a stream of content, it can be one of several types.
// This class serves as the base for all content types that can be returned in a streaming response.
// The Polymorphic discriminator is used to determine the specific type of content being returned.
// This allows us to handle different types of content in a unified way while still being able to
// differentiate between them based on the "type" property.
// Example: { "type": "answer", "text": "This is the answer content." }
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(AnswerContent), typeDiscriminator: "answer")]
[JsonDerivedType(typeof(RetrievalContent), typeDiscriminator: "retrieval")]
[JsonDerivedType(typeof(CitationsContent), typeDiscriminator: "citations")]
[JsonDerivedType(typeof(StatusContent), typeDiscriminator: "status")]
[JsonDerivedType(typeof(MetaDataContent), typeDiscriminator: "metadata")]
[JsonDerivedType(typeof(DebugContent), typeDiscriminator: "debug")]
[JsonDerivedType(typeof(AugmentedContext), typeDiscriminator: "augmented_context")]

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
