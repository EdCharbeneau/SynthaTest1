namespace ProgressSyntha.Models.Streaming;

public class AnswerContent : RAGContent
{
	public string? Text { get; set; }
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

public class AugmentedContext : RAGContent
{
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
