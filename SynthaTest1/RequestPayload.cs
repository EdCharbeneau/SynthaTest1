using System.Net.Http.Json;
using System.Text.Json;
public class RequestPayload
{
    public string query { get; set; }
    public object[] context { get; set; } = Array.Empty<object>();
    public string[] show { get; set; }
    public string[] features { get; set; }
    public bool highlight { get; set; }
    public bool citations { get; set; }
    public bool rephrase { get; set; }
    public bool debug { get; set; }
    public bool show_hidden { get; set; }
    public string reranker { get; set; }
    public bool autofilter { get; set; }
    public object[] filters { get; set; } = Array.Empty<object>();
    public RagStrategy[] rag_strategies { get; set; }
}

public class RagStrategy
{
    public string name { get; set; }
    public int before { get; set; }
    public int after { get; set; }
}

public class StreamResponse
{
    public Item item { get; set; }
}

public class Item
{
    public string type { get; set; }
    public string text { get; set; }
}



