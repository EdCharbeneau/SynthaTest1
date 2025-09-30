using ProgressSyntha.Models;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Web;
using static System.Net.WebRequestMethods;

namespace ProgressSyntha.Services;

/// <summary>
/// Implementation of Search service operations
/// </summary>
internal class SearchService : ISearchService
{
	private readonly HttpClient _httpClient;
	private readonly string _baseUrl;
	private readonly JsonSerializerOptions _jsonOptions;
	private readonly string _knowledgeBaseId;

	public SearchService(HttpClient httpClient, string baseUrl, JsonSerializerOptions jsonOptions, string knowledgeBaseId)
	{
		_httpClient = httpClient;
		_baseUrl = baseUrl;
		_jsonOptions = jsonOptions;
		_knowledgeBaseId = knowledgeBaseId;
	}

	/// <inheritdoc />
	public async Task<ApiResponse<SyncAskResponse>> AskAsync(AskRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			// Set synchronous header for non-streaming response
			using var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/kb/{_knowledgeBaseId}/ask");
			httpRequest.Headers.Add("x-synchronous", "true");
			httpRequest.Content = JsonContent.Create(request, options: _jsonOptions);

			var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

			if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
			{
				var validationError = await response.Content.ReadFromJsonAsync<HttpValidationError>(_jsonOptions, cancellationToken);
				return ApiResponse<SyncAskResponse>.CreateValidationError(validationError!);
			}

			response.EnsureSuccessStatusCode();
			var askResponse = await response.Content.ReadFromJsonAsync<SyncAskResponse>(_jsonOptions, cancellationToken);
			return ApiResponse<SyncAskResponse>.CreateSuccess(askResponse!);
		}
		catch (HttpRequestException ex)
		{
			return ApiResponse<SyncAskResponse>.CreateError($"HTTP request failed: {ex.Message}");
		}
		catch (Exception ex)
		{
			return ApiResponse<SyncAskResponse>.CreateError($"Unexpected error: {ex.Message}");
		}
	}

	/// <inheritdoc />
	public async IAsyncEnumerable<StreamResponse> AskStreamAsync(AskRequest request, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		using var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/kb/{_knowledgeBaseId}/ask");
		httpRequest.Content = JsonContent.Create(request, options: _jsonOptions);
		var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

		response.EnsureSuccessStatusCode();

		await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
		using var reader = new StreamReader(stream);

		while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
		{
			var line = await reader.ReadLineAsync();
			if (string.IsNullOrEmpty(line)) continue;

			var streamResponse = JsonSerializer.Deserialize<StreamResponse>(line, _jsonOptions);
			if (streamResponse != null)
			{
				yield return streamResponse;
			}
		}
	}

	/// <inheritdoc />
	public async IAsyncEnumerable<StreamResponse> AskStreamAsync(string query, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		var defaultRequest = new AskRequest
		{
			Query = query
		};
		await foreach (var response in AskStreamAsync(defaultRequest, cancellationToken))
		{
			yield return response;
		}
	}

	/// <inheritdoc />
	public async Task<ApiResponse<KnowledgeboxFindResults>> FindAsync(string query, int pageNumber = 0, int pageSize = 20, CancellationToken cancellationToken = default)
	{
		try
		{
			var queryParams = HttpUtility.ParseQueryString(string.Empty);
			queryParams["query"] = query;
			queryParams["page_number"] = pageNumber.ToString();
			queryParams["top_k"] = pageSize.ToString();

			var url = $"{_baseUrl}/kb/{_knowledgeBaseId}/find";
			if (queryParams.Count > 0)
				url += "?" + queryParams.ToString();

			var response = await _httpClient.GetAsync(url, cancellationToken);

			if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
			{
				var validationError = await response.Content.ReadFromJsonAsync<HttpValidationError>(_jsonOptions, cancellationToken);
				return ApiResponse<KnowledgeboxFindResults>.CreateValidationError(validationError!);
			}

			response.EnsureSuccessStatusCode();
			var results = await response.Content.ReadFromJsonAsync<KnowledgeboxFindResults>(_jsonOptions, cancellationToken);
			return ApiResponse<KnowledgeboxFindResults>.CreateSuccess(results!);
		}
		catch (HttpRequestException ex)
		{
			return ApiResponse<KnowledgeboxFindResults>.CreateError($"HTTP request failed: {ex.Message}");
		}
		catch (Exception ex)
		{
			return ApiResponse<KnowledgeboxFindResults>.CreateError($"Unexpected error: {ex.Message}");
		}
	}

	/// <inheritdoc />
	public async Task<ApiResponse<KnowledgeboxFindResults>> FindAsync(FindRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/kb/{_knowledgeBaseId}/find", request, _jsonOptions, cancellationToken);

			if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
			{
				var validationError = await response.Content.ReadFromJsonAsync<HttpValidationError>(_jsonOptions, cancellationToken);
				return ApiResponse<KnowledgeboxFindResults>.CreateValidationError(validationError!);
			}

			response.EnsureSuccessStatusCode();
			var results = await response.Content.ReadFromJsonAsync<KnowledgeboxFindResults>(_jsonOptions, cancellationToken);
			return ApiResponse<KnowledgeboxFindResults>.CreateSuccess(results!);
		}
		catch (HttpRequestException ex)
		{
			return ApiResponse<KnowledgeboxFindResults>.CreateError($"HTTP request failed: {ex.Message}");
		}
		catch (Exception ex)
		{
			return ApiResponse<KnowledgeboxFindResults>.CreateError($"Unexpected error: {ex.Message}");
		}
	}

	/// <inheritdoc />
	public async Task<ApiResponse<KnowledgeboxSearchResults>> CatalogAsync(string query = "", int pageNumber = 0, int pageSize = 20, CancellationToken cancellationToken = default)
	{
		try
		{
			var queryParams = HttpUtility.ParseQueryString(string.Empty);
			if (!string.IsNullOrEmpty(query))
				queryParams["query"] = query;
			queryParams["page_number"] = pageNumber.ToString();
			queryParams["page_size"] = pageSize.ToString();

			var url = $"{_baseUrl}/kb/{_knowledgeBaseId}/catalog";
			if (queryParams.Count > 0)
				url += "?" + queryParams.ToString();

			var response = await _httpClient.GetAsync(url, cancellationToken);

			if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
			{
				var validationError = await response.Content.ReadFromJsonAsync<HttpValidationError>(_jsonOptions, cancellationToken);
				return ApiResponse<KnowledgeboxSearchResults>.CreateValidationError(validationError!);
			}

			response.EnsureSuccessStatusCode();
			var results = await response.Content.ReadFromJsonAsync<KnowledgeboxSearchResults>(_jsonOptions, cancellationToken);
			return ApiResponse<KnowledgeboxSearchResults>.CreateSuccess(results!);
		}
		catch (HttpRequestException ex)
		{
			return ApiResponse<KnowledgeboxSearchResults>.CreateError($"HTTP request failed: {ex.Message}");
		}
		catch (Exception ex)
		{
			return ApiResponse<KnowledgeboxSearchResults>.CreateError($"Unexpected error: {ex.Message}");
		}
	}

	/// <inheritdoc />
	public async Task<ApiResponse<KnowledgeboxSearchResults>> CatalogAsync(CatalogRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/kb/{_knowledgeBaseId}/catalog", request, _jsonOptions, cancellationToken);

			if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
			{
				var validationError = await response.Content.ReadFromJsonAsync<HttpValidationError>(_jsonOptions, cancellationToken);
				return ApiResponse<KnowledgeboxSearchResults>.CreateValidationError(validationError!);
			}

			response.EnsureSuccessStatusCode();
			var results = await response.Content.ReadFromJsonAsync<KnowledgeboxSearchResults>(_jsonOptions, cancellationToken);
			return ApiResponse<KnowledgeboxSearchResults>.CreateSuccess(results!);
		}
		catch (HttpRequestException ex)
		{
			return ApiResponse<KnowledgeboxSearchResults>.CreateError($"HTTP request failed: {ex.Message}");
		}
		catch (Exception ex)
		{
			return ApiResponse<KnowledgeboxSearchResults>.CreateError($"Unexpected error: {ex.Message}");
		}
	}
}
