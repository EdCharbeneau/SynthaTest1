using ProgressSyntha;
using ProgressSyntha.Models;

namespace ProgressSyntha.Examples;

/// <summary>
/// Example usage of the NucliaDB SDK
/// </summary>
public class SdkUsageExample
{
    public static async Task RunExamplesAsync()
    {
        // Configuration
        var config = new SynthaConfig("your-zone-id", "your-kb-id", "your-api-key");
        
        // Create client
        using var client = new NucliaDbClient(config);

        // Example 1: Get Knowledge Box information
        Console.WriteLine("=== Getting Knowledge Box Information ===");
        var kbResult = await client.KnowledgeBoxes.GetKnowledgeBoxAsync(config.KnowledgeBaseId);
        if (kbResult.Success && kbResult.Data != null)
        {
            Console.WriteLine($"Knowledge Box: {kbResult.Data.Title}");
            Console.WriteLine($"Description: {kbResult.Data.Description}");
        }
        else
        {
            Console.WriteLine($"Error: {kbResult.Error}");
        }

        // Example 2: Ask a question (synchronous)
        Console.WriteLine("\n=== Asking a Question (Synchronous) ===");
        var askRequest = new AskRequest
        {
            Query = "What is artificial intelligence?",
            TopK = 10,
            Citations = true,
            Debug = false
        };        var askResult = await client.Search.AskAsync(askRequest);
        if (askResult.Success && askResult.Data != null)
        {
            Console.WriteLine($"Answer: {askResult.Data.Answer}");
            Console.WriteLine($"Status: {askResult.Data.Status}");
            if (askResult.Data.RetrievalBestMatches != null)
            {
                Console.WriteLine($"Found {askResult.Data.RetrievalBestMatches.Length} relevant results");
            }
        }
        else
        {
            Console.WriteLine($"Error: {askResult.Error}");
        }

        // Example 3: Ask a question (streaming)
        Console.WriteLine("\n=== Asking a Question (Streaming) ===");
        var streamRequest = new AskRequest
        {
            Query = "Explain machine learning",
            TopK = 5
        };

        await foreach (var response in client.Search.AskStreamAsync(streamRequest))
        {
            if (!string.IsNullOrEmpty(response.Data))
            {
                Console.Write(response.Data);
            }
        }
        Console.WriteLine(); // New line after streaming

        // Example 4: Search/Catalog resources
        Console.WriteLine("\n=== Searching Resources ===");
        var catalogResult = await client.Search.CatalogAsync("machine learning", pageSize: 5);
        if (catalogResult.Success && catalogResult.Data != null)
        {
            Console.WriteLine($"Found {catalogResult.Data.Total} total resources");
            foreach (var resource in catalogResult.Data.Resources.Take(3))
            {
                Console.WriteLine($"- {resource.Title} (ID: {resource.Id})");
                Console.WriteLine($"  Created: {resource.Created:yyyy-MM-dd}");
                Console.WriteLine($"  Summary: {(resource.Summary?.Length > 100 ? resource.Summary.Substring(0, 100) : resource.Summary ?? "No summary")}...");
            }
        }
        else
        {
            Console.WriteLine($"Error: {catalogResult.Error}");
        }

        // Example 5: Advanced catalog search with filters
        Console.WriteLine("\n=== Advanced Catalog Search ===");
        var advancedCatalogRequest = new CatalogRequest
        {
            Query = "technology",
            PageSize = 10,
            SortField = SortField.Modified,
            SortOrder = SortOrder.Desc,
            Show = new[] { ResourceProperties.Basic, ResourceProperties.Values, ResourceProperties.Origin }
        };

        var advancedCatalogResult = await client.Search.CatalogAsync(advancedCatalogRequest);
        if (advancedCatalogResult.Success && advancedCatalogResult.Data != null)
        {
            Console.WriteLine($"Advanced search found {advancedCatalogResult.Data.Total} resources");
            foreach (var resource in advancedCatalogResult.Data.Resources.Take(2))
            {
                Console.WriteLine($"- {resource.Title}");
                Console.WriteLine($"  Modified: {resource.Modified:yyyy-MM-dd HH:mm}");
                Console.WriteLine($"  Status: {resource.Status}");
            }
        }
        else
        {
            Console.WriteLine($"Error: {advancedCatalogResult.Error}");
        }

        Console.WriteLine("\n=== Examples Complete ===");
    }

    /// <summary>
    /// Example of error handling
    /// </summary>
    public static async Task ErrorHandlingExampleAsync()
    {
        var config = new SynthaConfig("invalid-zone", "invalid-kb", "invalid-key");
        using var client = new NucliaDbClient(config);

        var result = await client.KnowledgeBoxes.GetKnowledgeBoxAsync("invalid-id");
        
        if (!result.Success)
        {
            if (result.ValidationError != null)
            {
                Console.WriteLine("Validation errors:");
                if (result.ValidationError.Detail != null)
                {
                    foreach (var error in result.ValidationError.Detail)
                    {
                        Console.WriteLine($"- {error.Msg} (Type: {error.Type})");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: {result.Error}");
            }
        }
    }
}
