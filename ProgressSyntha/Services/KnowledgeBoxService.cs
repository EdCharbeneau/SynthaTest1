using ProgressSyntha.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace ProgressSyntha.Services;

/// <summary>
/// Implementation of Knowledge Box service operations
/// </summary>
internal class KnowledgeBoxService : IKnowledgeBoxService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _jsonOptions;

    public KnowledgeBoxService(HttpClient httpClient, string baseUrl, JsonSerializerOptions jsonOptions)
    {
        _httpClient = httpClient;
        _baseUrl = baseUrl;
        _jsonOptions = jsonOptions;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<KnowledgeBoxObj>> GetKnowledgeBoxAsync(string knowledgeBoxId, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/kb/{knowledgeBoxId}", cancellationToken);
            
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var validationError = await response.Content.ReadFromJsonAsync<HttpValidationError>(_jsonOptions, cancellationToken);
                return ApiResponse<KnowledgeBoxObj>.CreateValidationError(validationError!);
            }

            response.EnsureSuccessStatusCode();
            var knowledgeBox = await response.Content.ReadFromJsonAsync<KnowledgeBoxObj>(_jsonOptions, cancellationToken);
            return ApiResponse<KnowledgeBoxObj>.CreateSuccess(knowledgeBox!);
        }
        catch (HttpRequestException ex)
        {
            return ApiResponse<KnowledgeBoxObj>.CreateError($"HTTP request failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            return ApiResponse<KnowledgeBoxObj>.CreateError($"Unexpected error: {ex.Message}");
        }
    }

    /// <inheritdoc />
    public async Task<ApiResponse<KnowledgeBoxObj>> GetKnowledgeBoxBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/kb/s/{slug}", cancellationToken);
            
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var validationError = await response.Content.ReadFromJsonAsync<HttpValidationError>(_jsonOptions, cancellationToken);
                return ApiResponse<KnowledgeBoxObj>.CreateValidationError(validationError!);
            }

            response.EnsureSuccessStatusCode();
            var knowledgeBox = await response.Content.ReadFromJsonAsync<KnowledgeBoxObj>(_jsonOptions, cancellationToken);
            return ApiResponse<KnowledgeBoxObj>.CreateSuccess(knowledgeBox!);
        }
        catch (HttpRequestException ex)
        {
            return ApiResponse<KnowledgeBoxObj>.CreateError($"HTTP request failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            return ApiResponse<KnowledgeBoxObj>.CreateError($"Unexpected error: {ex.Message}");
        }
    }
}
