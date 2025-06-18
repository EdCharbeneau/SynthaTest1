using ProgressSyntha.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;

namespace ProgressSyntha.Services;

/// <summary>
/// Implementation of Resource service operations
/// </summary>
internal class ResourceService : IResourceService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly string _knowledgeBaseId;

    public ResourceService(HttpClient httpClient, string baseUrl, JsonSerializerOptions jsonOptions, string knowledgeBaseId)
    {
        _httpClient = httpClient;
        _baseUrl = baseUrl;
        _jsonOptions = jsonOptions;
        _knowledgeBaseId = knowledgeBaseId;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<KnowledgeboxResource>> GetResourceAsync(string resourceId, ResourceProperties[]? show = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var url = $"{_baseUrl}/kb/{_knowledgeBaseId}/resource/{resourceId}";
            
            if (show != null && show.Length > 0)
            {
                var queryParams = HttpUtility.ParseQueryString(string.Empty);
                foreach (var prop in show)
                {
                    queryParams.Add("show", prop.ToString().ToLowerInvariant());
                }
                url += "?" + queryParams.ToString();
            }

            var response = await _httpClient.GetAsync(url, cancellationToken);
            
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var validationError = await response.Content.ReadFromJsonAsync<HttpValidationError>(_jsonOptions, cancellationToken);
                return ApiResponse<KnowledgeboxResource>.CreateValidationError(validationError!);
            }

            response.EnsureSuccessStatusCode();
            var resource = await response.Content.ReadFromJsonAsync<KnowledgeboxResource>(_jsonOptions, cancellationToken);
            return ApiResponse<KnowledgeboxResource>.CreateSuccess(resource!);
        }
        catch (HttpRequestException ex)
        {
            return ApiResponse<KnowledgeboxResource>.CreateError($"HTTP request failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            return ApiResponse<KnowledgeboxResource>.CreateError($"Unexpected error: {ex.Message}");
        }
    }

    /// <inheritdoc />
    public async Task<ApiResponse<bool>> DeleteResourceAsync(string resourceId, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/kb/{_knowledgeBaseId}/resource/{resourceId}", cancellationToken);
            
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var validationError = await response.Content.ReadFromJsonAsync<HttpValidationError>(_jsonOptions, cancellationToken);
                return ApiResponse<bool>.CreateValidationError(validationError!);
            }

            response.EnsureSuccessStatusCode();
            return ApiResponse<bool>.CreateSuccess(true);
        }
        catch (HttpRequestException ex)
        {
            return ApiResponse<bool>.CreateError($"HTTP request failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            return ApiResponse<bool>.CreateError($"Unexpected error: {ex.Message}");
        }
    }
}
