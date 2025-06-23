using ProgressSyntha.Models;
using System.Runtime.CompilerServices;

namespace ProgressSyntha.Services;

/// <summary>
/// Interface for Search operations
/// </summary>
public interface ISearchService
{
    /// <summary>
    /// Ask a question to the Knowledge Box and get a synchronous response
    /// </summary>
    /// <param name="request">Ask request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Synchronous ask response</returns>
    Task<ApiResponse<SyncAskResponse>> AskAsync(AskRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ask a question to the Knowledge Box and get a streaming response
    /// </summary>
    /// <param name="request">Ask request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Async enumerable of streaming responses</returns>
    IAsyncEnumerable<StreamResponse> AskStreamAsync(AskRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ask a question to the Knowledge Box and get a streaming response
    /// </summary>
    /// <param name="query">Query with default Ask request options.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Async enumerable of streaming responses</returns>
    IAsyncEnumerable<StreamResponse> AskStreamAsync(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find resources in the Knowledge Box using GET method with basic options
    /// </summary>
    /// <param name="query">Search query</param>
    /// <param name="pageNumber">Page number (default: 0)</param>
    /// <param name="pageSize">Page size (default: 20, max: 200)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Knowledge box find results with detailed paragraph information</returns>
    Task<ApiResponse<KnowledgeboxFindResults>> FindAsync(string query, int pageNumber = 0, int pageSize = 20, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find resources in the Knowledge Box using POST method with advanced options
    /// </summary>
    /// <param name="request">Find request with detailed search parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Knowledge box find results with detailed paragraph information</returns>
    Task<ApiResponse<KnowledgeboxFindResults>> FindAsync(FindRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// List resources in the Knowledge Box using GET method
    /// </summary>
    /// <param name="query">Search query</param>
    /// <param name="pageNumber">Page number (default: 0)</param>
    /// <param name="pageSize">Page size (default: 20, max: 200)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Knowledge box search results</returns>
    Task<ApiResponse<KnowledgeboxSearchResults>> CatalogAsync(string query = "", int pageNumber = 0, int pageSize = 20, CancellationToken cancellationToken = default);

    /// <summary>
    /// List resources in the Knowledge Box using POST method with advanced options
    /// </summary>
    /// <param name="request">Catalog request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Knowledge box search results</returns>
    Task<ApiResponse<KnowledgeboxSearchResults>> CatalogAsync(CatalogRequest request, CancellationToken cancellationToken = default);
}
