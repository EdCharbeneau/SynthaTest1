using ProgressSyntha.Models;

namespace ProgressSyntha.Services;

/// <summary>
/// Interface for Knowledge Box operations
/// </summary>
public interface IKnowledgeBoxService
{
    /// <summary>
    /// Get a Knowledge Box by its ID
    /// </summary>
    /// <param name="knowledgeBoxId">Knowledge Box ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Knowledge Box information</returns>
    Task<ApiResponse<KnowledgeBoxObj>> GetKnowledgeBoxAsync(string knowledgeBoxId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a Knowledge Box by its slug
    /// </summary>
    /// <param name="slug">Knowledge Box slug</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Knowledge Box information</returns>
    Task<ApiResponse<KnowledgeBoxObj>> GetKnowledgeBoxBySlugAsync(string slug, CancellationToken cancellationToken = default);
}
