using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ProgressSyntha.Services;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProgressSyntha;

/// <summary>
/// Main NucliaDB SDK client for interacting with the NucliaDB REST API
/// </summary>
public class SynthaClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly bool _disposeHttpClient;
    private readonly ILoggerFactory _loggerFactory;

    /// <summary>
    /// Configuration for the NucliaDB client
    /// </summary>
    public SynthaConfig Config { get; }

    /// <summary>
    /// Knowledge Box operations
    /// </summary>
    public IKnowledgeBoxService KnowledgeBoxes { get; }

    /// <summary>
    /// Search operations
    /// </summary>
    public ISearchService Search { get; }

    /// <summary>
    /// Resource operations
    /// </summary>
    public IResourceService Resources { get; }

    /// <summary>
    /// Creates a new NucliaDB client with the provided configuration
    /// </summary>
    /// <param name="config">Client configuration</param>
    /// <param name="loggerFactory">Optional logger factory for logging</param>
    public SynthaClient(SynthaConfig config, ILoggerFactory loggerFactory = null) 
        : this(new HttpClient(), config, true, loggerFactory)
    {
    }

    /// <summary>
    /// Creates a new NucliaDB client with the provided HttpClient and configuration
    /// </summary>
    /// <param name="httpClient">HTTP client to use</param>
    /// <param name="config">Client configuration</param>
    /// <param name="loggerFactory">Optional logger factory for logging</param>
    public SynthaClient(HttpClient httpClient, SynthaConfig config, ILoggerFactory loggerFactory = null) 
        : this(httpClient, config, false, loggerFactory)
    {
    }

    private SynthaClient(HttpClient httpClient, SynthaConfig config, bool disposeHttpClient, ILoggerFactory loggerFactory = null)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        Config = config ?? throw new ArgumentNullException(nameof(config));
        _disposeHttpClient = disposeHttpClient;
        _loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;

        // Configure HTTP client
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("X-NUCLIA-SERVICEACCOUNT", $"Bearer {config.ApiKey}");        // Configure JSON serialization
        _jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        // Initialize services
        var baseUrl = $"https://{config.ZoneId}.syntha.progress.com/api/v1";
        KnowledgeBoxes = new KnowledgeBoxService(_httpClient, baseUrl, _jsonOptions);
        Search = new SearchService(
            _httpClient, 
            baseUrl, 
            _jsonOptions, 
            config.KnowledgeBaseId);
        Resources = new ResourceService(_httpClient, baseUrl, _jsonOptions, config.KnowledgeBaseId);
    }    
    
    /// <summary>
    /// Disposes the HTTP client if it was created by this instance
    /// </summary>
    public void Dispose()
    {
        if (_disposeHttpClient)
        {
            _httpClient?.Dispose();
        }
    }
}
