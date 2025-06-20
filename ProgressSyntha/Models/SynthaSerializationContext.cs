using System.Text.Json.Serialization;

namespace ProgressSyntha.Models;

// This class configures JSON serialization to use source generation
// Source generation creates serialization code at compile time, eliminating reflection
// and improving performance across the application
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(SyncAskResponse))]
[JsonSerializable(typeof(RagStrategy))]
[JsonSerializable(typeof(Retrieval))]
[JsonSerializable(typeof(RetrievalResult))]
[JsonSerializable(typeof(RetrievalField))]
[JsonSerializable(typeof(RetrievalResults))]
[JsonSerializable(typeof(RetrievalRelations))]
[JsonSerializable(typeof(RetrievalMatch))]
[JsonSerializable(typeof(Answer))]
[JsonSerializable(typeof(Dictionary<string, object>))]
[JsonSerializable(typeof(Dictionary<string, string>))]
[JsonSerializable(typeof(Dictionary<string, int>))]
[JsonSerializable(typeof(List<string>))]
[JsonSerializable(typeof(List<object>))]
internal partial class ProgressSynthaModelsContext : JsonSerializerContext
{
}