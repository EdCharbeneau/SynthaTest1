using System.Net.Http.Json;
using System.Text.Json;
public class RequestPayload
{
	public string Query { get; set; }
	public object[] Context { get; set; } = Array.Empty<object>();
	public string[] Show { get; set; }
	public string[] Features { get; set; }
	public bool Highlight { get; set; }
	public bool Citations { get; set; }
	public bool Rephrase { get; set; }
	public bool Debug { get; set; }
	public bool ShowHidden { get; set; }
	public string Reranker { get; set; }
	public bool Autofilter { get; set; }
	public object[] Filters { get; set; } = Array.Empty<object>();
	public RagStrategy[] RagStrategies { get; set; }
}

