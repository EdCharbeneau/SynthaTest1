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
[JsonSerializable(typeof(FindRequest))]
[JsonSerializable(typeof(KnowledgeboxFindResults))]
[JsonSerializable(typeof(FindRelationsData))]
[JsonSerializable(typeof(FindEntityRelations))]
[JsonSerializable(typeof(FindEntityRelation))]
[JsonSerializable(typeof(FindFieldData))]
[JsonSerializable(typeof(FindParagraphData))]
[JsonSerializable(typeof(FindParagraphPosition))]
[JsonSerializable(typeof(FindRelevantRelations))]
[JsonSerializable(typeof(FindResourceMetadata))]
[JsonSerializable(typeof(FindUserMetadata))]
[JsonSerializable(typeof(FindClassificationItem))]
[JsonSerializable(typeof(FindFieldMetadataItem))]
[JsonSerializable(typeof(FindFieldInfo))]
[JsonSerializable(typeof(FindComputedMetadata))]
[JsonSerializable(typeof(FindFieldClassificationItem))]
[JsonSerializable(typeof(FindResourceOrigin))]
[JsonSerializable(typeof(FindResourceRelation))]
[JsonSerializable(typeof(FindRelationMetadata))]
[JsonSerializable(typeof(FindEntityInfo))]
[JsonSerializable(typeof(FindSecurityInfo))]
[JsonSerializable(typeof(Dictionary<string, object>))]
[JsonSerializable(typeof(Dictionary<string, string>))]
[JsonSerializable(typeof(Dictionary<string, int>))]
[JsonSerializable(typeof(Dictionary<string, double>))]
[JsonSerializable(typeof(Dictionary<string, FindFieldData>))]
[JsonSerializable(typeof(Dictionary<string, FindParagraphData>))]
[JsonSerializable(typeof(Dictionary<string, FindResource>))]
[JsonSerializable(typeof(Dictionary<string, FindEntityRelations>))]
[JsonSerializable(typeof(List<string>))]
[JsonSerializable(typeof(List<object>))]
[JsonSerializable(typeof(List<Dictionary<string, string>>))]
[JsonSerializable(typeof(string[]))]
[JsonSerializable(typeof(object[]))]
[JsonSerializable(typeof(FindClassificationItem[]))]
[JsonSerializable(typeof(FindFieldClassificationItem[]))]
[JsonSerializable(typeof(FindFieldMetadataItem[]))]
[JsonSerializable(typeof(FindResourceRelation[]))]
internal partial class ProgressSynthaModelsContext : JsonSerializerContext
{
}