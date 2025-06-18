using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using ProgressSyntha.Models;

namespace ProgressSyntha;

/// <summary>
/// Legacy SynthaClient - use NucliaDbClient for new applications
/// </summary>
[Obsolete("Use NucliaDbClient instead. This class is maintained for backward compatibility.")]
public class SynthaClient
{
	private readonly HttpClient http;
	private readonly SynthaConfig config;
	public SynthaClient(HttpClient http, SynthaConfig config)
	{
		this.http = http ?? throw new ArgumentNullException(nameof(http));
		this.config = config;
		http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		http.DefaultRequestHeaders.Add("X-NUCLIA-SERVICEACCOUNT", $"Bearer {config.ApiKey}");

	}

	public SynthaClient(SynthaConfig config) : this(new HttpClient(), config)
	{
	}

	private readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web);
	private string Endpoint => $"https://{config.ZoneId}.syntha.progress.com/api/v1/kb/{config.KnowledgeBaseId}/ask";

	private AskRequest defaultOptions = new AskRequest
	{
		Query = string.Empty,
		Show = new[] { "basic", "values", "origin" },
		Features = new[] { "keyword", "semantic" },
		Highlight = false,
		Citations = true,
		Rephrase = true,
		Debug = true,
		ShowHidden = false,
		Reranker = "predict",
		Autofilter = false,
		RagStrategies = new[]
		{
			new RagStrategy
			{
				Name = "neighbouring_paragraphs",
				Before = 2,
				After = 2
			}
		},
		Context = Array.Empty<object>(),
		Filters = Array.Empty<object>()
	};

	public async IAsyncEnumerable<StreamResponse> Ask(string query = "What is syntha", [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		var requestOptions = defaultOptions with { Query = query };

		using var response = await http.PostAsJsonAsync(Endpoint, requestOptions, jsonOptions, cancellationToken);
		response.EnsureSuccessStatusCode();

		await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
		using var reader = new StreamReader(stream);

		while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
		{
			var line = await reader.ReadLineAsync();
			if (string.IsNullOrEmpty(line)) continue;

			var streamResponse = JsonSerializer.Deserialize<StreamResponse>(line, jsonOptions);
			if (streamResponse != null)
			{
				yield return streamResponse;
			}
		}
	}
}
