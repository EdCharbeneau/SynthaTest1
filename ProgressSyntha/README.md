# NucliaDB C# SDK

A comprehensive C# SDK for interacting with the NucliaDB REST API, providing easy access to Knowledge Box operations, search functionality, and resource management.

## Features

- **Knowledge Box Operations**: Get Knowledge Box information by ID or slug
- **Search & Ask**: Ask questions with both synchronous and streaming responses
- **Resource Catalog**: Search and list resources with advanced filtering
- **Resource Management**: Get and manage individual resources
- **Type Safety**: Strongly-typed models for all API responses
- **Error Handling**: Comprehensive error handling with validation error support
- **Async/Await**: Full async support throughout the SDK
- **Streaming**: Support for real-time streaming responses

## Installation

Add the ProgressSyntha project to your solution and reference it in your project.

## Quick Start

### Basic Configuration

```csharp
using ProgressSyntha;
using ProgressSyntha.Models;

// Configure the client
var config = new SynthaConfig("your-zone-id", "your-knowledge-base-id", "your-api-key");

// Create the client
using var client = new NucliaDbClient(config);
```

### Ask a Question (Synchronous)

```csharp
var askRequest = new AskRequest
{
    Query = "What is artificial intelligence?",
    TopK = 10,
    Citations = true
};

var result = await client.Search.AskAsync(askRequest);
if (result.Success && result.Data != null)
{
    Console.WriteLine($"Answer: {result.Data.Answer?.Text}");
    Console.WriteLine($"Found {result.Data.Retrieval?.Results?.Length} relevant sources");
}
```

### Ask a Question (Streaming)

```csharp
var askRequest = new AskRequest
{
    Query = "Explain machine learning",
    TopK = 5
};

await foreach (var response in client.Search.AskStreamAsync(askRequest))
{
    if (!string.IsNullOrEmpty(response.Data))
    {
        Console.Write(response.Data);
    }
}
```

### Search Resources

```csharp
// Simple search
var catalogResult = await client.Search.CatalogAsync("machine learning", pageSize: 10);

// Advanced search with filters
var advancedRequest = new CatalogRequest
{
    Query = "technology",
    PageSize = 20,
    SortField = SortField.Modified,
    SortOrder = SortOrder.Desc,
    Show = new[] { ResourceProperties.Basic, ResourceProperties.Values }
};

var advancedResult = await client.Search.CatalogAsync(advancedRequest);
```

### Get Knowledge Box Information

```csharp
var kbResult = await client.KnowledgeBoxes.GetKnowledgeBoxAsync("your-kb-id");
if (kbResult.Success && kbResult.Data != null)
{
    Console.WriteLine($"Knowledge Box: {kbResult.Data.Title}");
    Console.WriteLine($"Description: {kbResult.Data.Description}");
}
```

## API Reference

### NucliaDbClient

The main client class that provides access to all SDK functionality.

#### Constructor
- `NucliaDbClient(SynthaConfig config)` - Creates a new client with default HttpClient
- `NucliaDbClient(HttpClient httpClient, SynthaConfig config)` - Creates a client with custom HttpClient

#### Properties
- `KnowledgeBoxes` - Access to Knowledge Box operations
- `Search` - Access to search and ask operations  
- `Resources` - Access to resource management operations

### Configuration

#### SynthaConfig
- `ApiKey` - Your API key for authentication
- `ZoneId` - The zone ID for your API endpoint
- `KnowledgeBaseId` - The Knowledge Base ID to work with

### Services

#### IKnowledgeBoxService
- `GetKnowledgeBoxAsync(string knowledgeBoxId)` - Get KB by ID
- `GetKnowledgeBoxBySlugAsync(string slug)` - Get KB by slug

#### ISearchService
- `AskAsync(AskRequest request)` - Ask question synchronously
- `AskStreamAsync(AskRequest request)` - Ask question with streaming
- `CatalogAsync(string query, int pageNumber, int pageSize)` - Simple catalog search
- `CatalogAsync(CatalogRequest request)` - Advanced catalog search

#### IResourceService
- `GetResourceAsync(string resourceId, ResourceProperties[] show)` - Get resource by ID
- `DeleteResourceAsync(string resourceId)` - Delete resource

### Models

#### AskRequest
Core properties for ask requests:
- `Query` - The question to ask
- `TopK` - Number of top results to retrieve (default: 20)  
- `Citations` - Include citations (default: true)
- `Features` - Search features to use (default: keyword, semantic)
- `RagStrategies` - RAG strategies for context retrieval

#### SyncAskResponse
- `Answer` - The generated answer
- `Retrieval` - Retrieved context information
- `Citations` - Citation information
- `Status` - Response status

#### CatalogRequest
- `Query` - Search query
- `PageNumber` - Page number (default: 0)
- `PageSize` - Results per page (default: 20, max: 200)
- `SortField` - Field to sort by
- `SortOrder` - Sort order (asc/desc)
- `Filters` - Advanced filters to apply

### Error Handling

All API methods return `ApiResponse<T>` which includes:
- `Success` - Whether the operation succeeded
- `Data` - The response data (if successful)
- `Error` - Error message (if failed)
- `ValidationError` - Detailed validation errors (if applicable)

```csharp
var result = await client.Search.AskAsync(request);
if (!result.Success)
{
    if (result.ValidationError != null)
    {
        // Handle validation errors
        foreach (var error in result.ValidationError.Detail ?? Array.Empty<ValidationErrorDetail>())
        {
            Console.WriteLine($"Validation error: {error.Msg}");
        }
    }
    else
    {
        // Handle general errors
        Console.WriteLine($"Error: {result.Error}");
    }
}
```

## Examples

See the `Examples/SdkUsageExample.cs` file for comprehensive usage examples including:
- Basic ask operations
- Streaming responses  
- Resource searching and filtering
- Error handling patterns
- Advanced configuration options

## Legacy Support

The original `SynthaClient` class is still available for backward compatibility but is marked as obsolete. New applications should use `NucliaDbClient`.

## Requirements

- .NET 9.0 or later
- System.Text.Json for JSON serialization
- System.Net.Http for HTTP operations

## License

This SDK is part of the Progress Syntha project.
