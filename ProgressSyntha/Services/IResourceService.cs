using ProgressSyntha.Models;

namespace ProgressSyntha.Services;

/// <summary>
/// Interface for Resource operations
/// </summary>
public interface IResourceService
{
    /// <summary>
    /// Get a resource by its ID
    /// </summary>
    /// <param name="resourceId">Resource ID</param>
    /// <param name="show">Properties to include in the response</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Resource information</returns>
    Task<ApiResponse<KnowledgeboxResource>> GetResourceAsync(string resourceId, ResourceProperties[]? show = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a resource by its ID
    /// </summary>
    /// <param name="resourceId">Resource ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success status</returns>
    Task<ApiResponse<bool>> DeleteResourceAsync(string resourceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Download field binary field by ID
    /// </summary>
    /// <param name="resourceId">Resource ID</param>
    /// <param name="fieldId">Field ID</param>
    /// <param name="inline">Whether to display the file inline</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Binary file content</returns>
    Task<ApiResponse<byte[]>> DownloadFieldFileAsync(string resourceId, string fieldId, bool inline = false, CancellationToken cancellationToken = default);    /// <summary>
    /// Download field binary field by URL
    /// </summary>
    /// <param name="resourceUrl">Complete resource URL path</param>
    /// <param name="inline">Whether to display the file inline</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Binary file content</returns>
    Task<ApiResponse<byte[]>> DownloadFieldFileAsync(string resourceUrl, bool inline = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a resource by its slug
    /// </summary>
    /// <param name="resourceSlug">Resource slug</param>
    /// <param name="show">Properties to include in the response</param>
    /// <param name="fieldType">Field types to include</param>
    /// <param name="extracted">Extracted data types to include</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Resource information</returns>
    Task<ApiResponse<KnowledgeboxResource>> GetResourceBySlugAsync(string resourceSlug, ResourceProperties[]? show = null, string[]? fieldType = null, string[]? extracted = null, CancellationToken cancellationToken = default);
}
