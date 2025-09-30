using System.Text.Json.Serialization;

namespace ProgressSyntha.Models.Streaming;

public class AnswerContent : RAGContent
{
	[JsonPropertyName("text")]
	public string? Text { get; set; }
}

public class StatusContent : RAGContent
{
	[JsonPropertyName("status")]
	public string? Status { get; set; }
	
	[JsonPropertyName("code")]
	public int Code { get; set; }
}

public class MetaDataContent : RAGContent
{
	[JsonPropertyName("tokens")]
	public Tokens? Tokens { get; set; }
	
	[JsonPropertyName("timings")]
	public Timings? Timings { get; set; }
}

public class AugmentedContext : RAGContent
{
}

public class Tokens
{
	[JsonPropertyName("input")]
	public int Input { get; set; }
	
	[JsonPropertyName("output")]
	public int Output { get; set; }
	
	[JsonPropertyName("input_nuclia")]
	public double InputNuclia { get; set; }
	
	[JsonPropertyName("output_nuclia")]
	public double OutputNuclia { get; set; }
}

public class Timings
{
	[JsonPropertyName("generative_first_chunk")]
	public double GenerativeFirstChunk { get; set; }
	
	[JsonPropertyName("generative_total")]
	public double GenerativeTotal { get; set; }
}
