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
}
