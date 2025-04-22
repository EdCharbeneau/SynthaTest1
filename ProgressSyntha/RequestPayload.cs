using System.Net.Http.Json;
using System.Text.Json;
public record RequestPayload(string Query, string[] Show, string[] Features, bool Highlight, bool Citations, bool Rephrase, bool Debug, bool ShowHidden, string Reranker, bool Autofilter, RagStrategy[] RagStrategies,
	object[] Context, object[] Filters);
