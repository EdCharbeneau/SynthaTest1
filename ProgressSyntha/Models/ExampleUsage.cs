using ProgressSyntha.Models;
using ProgressSyntha.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProgressSyntha;

/// <summary>
/// Examples showing how to use the Find API
/// </summary>
public static class FindApiExamples
{
    /// <summary>
    /// Simple example of using the Find API with a basic query
    /// </summary>
    public static async Task SimpleQueryExample(ISearchService searchService)
    {
        // Use the simple Find method with a basic query
        var result = await searchService.FindAsync("What is Syntha?");

        if (result.Success && result.Data != null)
        {
            Console.WriteLine($"Found {result.Data.Total} results for query: {result.Data.Query}");
            Console.WriteLine($"Page {result.Data.PageNumber} of {Math.Ceiling((double)result.Data.Total / result.Data.PageSize)}");
            
            if (result.Data.Resources.Count > 0)
            {
                // Display basic information about the resources
                foreach (var resource in result.Data.Resources.Values)
                {
                    Console.WriteLine($"\nResource: {resource.Title}");
                    Console.WriteLine($"ID: {resource.Id}, Type: {resource.Icon}");

                    if (resource.Fields != null)
                    {
                        foreach (var field in resource.Fields)
                        {
                            Console.WriteLine($"\nField: {field.Key}");
                            
                            // Display paragraphs for each field
                            foreach (var paragraph in field.Value.Paragraphs.Values.OrderBy(p => p.Order))
                            {
                                Console.WriteLine($"  - Score: {paragraph.Score} ({paragraph.ScoreTypeString})");
                                Console.WriteLine($"    Text: {paragraph.Text?.Substring(0, Math.Min(paragraph.Text.Length, 100))}...");
                                
                                if (paragraph.Labels?.Length > 0)
                                {
                                    Console.WriteLine($"    Labels: {string.Join(", ", paragraph.Labels)}");
                                }
                            }
                        }
                    }
                }
            }
            
            // Check for any best matches
            if (result.Data.BestMatches?.Length > 0)
            {
                Console.WriteLine("\nBest matches:");
                foreach (var match in result.Data.BestMatches)
                {
                    Console.WriteLine($"  - {match}");
                }
            }
        }
        else if (result.ValidationError != null)
        {
            Console.WriteLine("Validation error:");
            foreach (var error in result.ValidationError.Detail ?? Array.Empty<ValidationErrorDetail>())
            {
                Console.WriteLine($" - {error.Msg}");
            }
        }
        else
        {
            Console.WriteLine($"Error: {result.Error}");
        }
    }

    /// <summary>
    /// Advanced example using more Find API options
    /// </summary>
    public static async Task AdvancedQueryExample(ISearchService searchService)
    {
        // Create a more complex Find request with advanced options
        var request = new FindRequest
        {
            Query = "Syntha RAG platform",
            TopK = 10,
            Features = new[] { Models.SearchOption.Semantic, Models.SearchOption.Keyword },
            MinScoreSemantic = 0.6,
            Show = new[] { ResourceProperties.Basic, ResourceProperties.Fields },
            FieldTypes = new[] { FieldType.Text, FieldType.File },
            Highlight = true,
            WithSynonyms = true
        };

        var result = await searchService.FindAsync(request);

        if (result.Success && result.Data != null)
        {
            Console.WriteLine($"Advanced search found {result.Data.Total} results");
            
            // Extract top paragraphs across all resources
            var allParagraphs = result.Data.Resources.Values
                .Where(r => r.Fields != null)
                .SelectMany(r => 
                    r.Fields.SelectMany(f => 
                        f.Value.Paragraphs.Values.Select(p => 
                            new 
                            { 
                                ResourceId = r.Id,
                                ResourceTitle = r.Title,
                                FieldId = f.Key,
                                Paragraph = p 
                            })))
                .OrderByDescending(p => p.Paragraph.Score)
                .Take(5)
                .ToList();
                
            Console.WriteLine("\nTop 5 paragraphs across all resources:");
            
            foreach (var item in allParagraphs)
            {
                Console.WriteLine($"\nResource: {item.ResourceTitle}");
                Console.WriteLine($"Field: {item.FieldId}");
                Console.WriteLine($"Score: {item.Paragraph.Score} ({item.Paragraph.ScoreTypeString})");
                Console.WriteLine($"Text: {item.Paragraph.Text}");
            }
        }
    }
}