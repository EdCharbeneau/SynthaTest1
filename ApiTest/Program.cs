using System.Text.Json;
using System.Net.Http.Json;

// Simple test to understand API response structure
var config = (
    ZoneId: "progress-proc-us-east-2-1",
    KnowledgeBaseId: "886a82a2-b0d6-400d-9907-9b8c0567681a",
    ApiKey: "eyJhbGciOiJSUzI1NiIsImtpZCI6InNhIiwidHlwIjoiSldUIn0.eyJpc3MiOiJodHRwczovL3Byb2dyZXNzLXByb2MtdXMtZWFzdC0yLTEuc3ludGhhLnByb2dyZXNzLmNvbS8iLCJpYXQiOjE3NDQ5MDUwODAsInN1YiI6IjE3M2VhZWViLWIxZDctNDYxNi1iOTY2LTY1ODZhZTJhOGRiNiIsImp0aSI6IjA2ZmJmOTQxLWYzMGYtNGIyMC05YjBhLTFkZDJkZWQyZWJiNCIsImV4cCI6MTc3NjQ0MTA3OCwia2V5IjoiODY3YWYzYWUtZjQ0OC00NjQxLTk1ZmEtNzg0NmVmMjQ0YmFlIiwia2lkIjoiOTI2MTQwZGYtNTU5OS00MTVkLWI4YzYtYjdjYjM0YmZjZjYxIn0.b2K9V0y2DhGvSj8ZiraQFO3bG8fyPU68DrKaIfw9-HV2qBWbFXEq2DA689RfRHSvKwiUM8d3l8PEbB_G-SzgWYGQdXhTlN1iE3LbSiGaseVAPuKfqU2v6rXH3ayQ9D4QuZUTnXpJziM97WUntmdUsnNOwr3WQ8okLWYf5cGhuefQi7meZ1xAnpxW99vkQVA0gOh_JBhAZfDgESZQESYgpQr-Iq1u-YKObBbP4R7Y_l3ZcjpW4Ne5LXgKlfkhDmc8cRx-2tv1izA2zccyFlQzzWPrAoLBdrcuKVqlFlfd1LhuNIS8tTTXQZuZRzSv2Nh0PP23z_Pd38L0fzrBYmUkkL_9BkI5XbO8EVGi_8Q2ESPcoUNaKYwDQPhalWivFKnyEdJUsNOXBgxnAyVOGuJVbrNJfyLNZ0K_eBBbtpoQTx7JsBIwAGHSKmW3H_n6KzNsmwKf-vhEzhxt_qAM_qmsfXSMfxe2eRFAPxz-vBfDFpfJ3epxtmXdrgwSfbfSLFLWTFId5f5hsvh_DahCBMJZhKvvCuPva8UiS-jAlf5DKH49cp7w6D35Hy1LmLXj5VmOOHr1hhu6gG0Im8hQEqacSBoMjXtj98bl1l0e6M_JUcBcawFzeXnEMRcoghns-agy9mv809nTlXir9Kb3AwguviXIaRhyi7rW9AruxFUu5Iw"
);

Console.WriteLine("Testing API response structure...");

try
{
    using var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Add("X-NUCLIA-SERVICEACCOUNT", $"Bearer {config.ApiKey}");
    httpClient.DefaultRequestHeaders.Add("x-synchronous", "true");
    
    var askRequest = new 
    {
        query = "What is Progress?",
        top_k = 2,
        show = new[] { "basic" },
        features = new[] { "semantic" }
    };
    
    var endpoint = $"https://{config.ZoneId}.syntha.progress.com/api/v1/kb/{config.KnowledgeBaseId}/ask";
    var response = await httpClient.PostAsJsonAsync(endpoint, askRequest);
    
    Console.WriteLine($"Status: {response.StatusCode}");
    
    if (response.IsSuccessStatusCode)
    {
        var jsonString = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Raw JSON Response (first 1000 chars):");
        Console.WriteLine(jsonString.Substring(0, Math.Min(1000, jsonString.Length)));
        Console.WriteLine();
        
        // Try to parse as JsonDocument to see structure
        using var doc = JsonDocument.Parse(jsonString);
        Console.WriteLine("JSON Structure:");
        PrintJsonElement(doc.RootElement, 0);
    }
    else
    {
        Console.WriteLine($"HTTP Error: {response.StatusCode}");
        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error content: {error}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

static void PrintJsonElement(JsonElement element, int indent)
{
    var indentStr = new string(' ', indent * 2);
    
    switch (element.ValueKind)
    {
        case JsonValueKind.Object:
            Console.WriteLine($"{indentStr}{{");
            foreach (var property in element.EnumerateObject().Take(5)) // Limit for readability
            {
                Console.WriteLine($"{indentStr}  \"{property.Name}\":");
                PrintJsonElement(property.Value, indent + 2);
            }
            Console.WriteLine($"{indentStr}}}");
            break;
            
        case JsonValueKind.Array:
            Console.WriteLine($"{indentStr}[... {element.GetArrayLength()} items]");
            break;
            
        case JsonValueKind.String:
            var str = element.GetString();
            var preview = str?.Length > 50 ? str.Substring(0, 50) + "..." : str;
            Console.WriteLine($"{indentStr}\"{preview}\"");
            break;
            
        case JsonValueKind.Number:
            Console.WriteLine($"{indentStr}{element.GetRawText()}");
            break;
            
        case JsonValueKind.True:
        case JsonValueKind.False:
            Console.WriteLine($"{indentStr}{element.GetBoolean()}");
            break;
            
        case JsonValueKind.Null:
            Console.WriteLine($"{indentStr}null");
            break;
    }
}
