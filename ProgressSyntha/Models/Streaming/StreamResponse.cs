using ProgressSyntha.Models.Streaming;

namespace ProgressSyntha.Models;

/// <summary>
/// Stream response item for real-time ask responses
/// </summary>
public class StreamResponse
{
	public RAGContent Item { get; set; }
}
