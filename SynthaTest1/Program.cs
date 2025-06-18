using ProgressSyntha;
using ProgressSyntha.Models;
using Spectre.Console;
using Spectre.Console.Json;
using System.Text.Json;

var config = new SynthaConfig(ZoneId: "progress-proc-us-east-2-1",
	KnowledgeBaseId: "886a82a2-b0d6-400d-9907-9b8c0567681a",
	ApiKey: "eyJhbGciOiJSUzI1NiIsImtpZCI6InNhIiwidHlwIjoiSldUIn0.eyJpc3MiOiJodHRwczovL3Byb2dyZXNzLXByb2MtdXMtZWFzdC0yLTEuc3ludGhhLnByb2dyZXNzLmNvbS8iLCJpYXQiOjE3NDQ5MDUwODAsInN1YiI6IjE3M2VhZWViLWIxZDctNDYxNi1iOTY2LTY1ODZhZTJhOGRiNiIsImp0aSI6IjA2ZmJmOTQxLWYzMGYtNGIyMC05YjBhLTFkZDJkZWQyZWJiNCIsImV4cCI6MTc3NjQ0MTA3OCwia2V5IjoiODY3YWYzYWUtZjQ0OC00NjQxLTk1ZmEtNzg0NmVmMjQ0YmFlIiwia2lkIjoiOTI2MTQwZGYtNTU5OS00MTVkLWI4YzYtYjdjYjM0YmZjZjYxIn0.b2K9V0y2DhGvSj8ZiraQFO3bG8fyPU68DrKaIfw9-HV2qBWbFXEq2DA689RfRHSvKwiUM8d3l8PEbB_G-SzgWYGQdXhTlN1iE3LbSiGaseVAPuKfqU2v6rXH3ayQ9D4QuZUTnXpJziM97WUntmdUsnNOwr3WQ8okLWYf5cGhuefQi7meZ1xAnpxW99vkQVA0gOh_JBhAZfDgESZQESYgpQr-Iq1u-YKObBbP4R7Y_l3ZcjpW4Ne5LXgKlfkhDmc8cRx-2tv1izA2zccyFlQzzWPrAoLBdrcuKVqlFlfd1LhuNIS8tTTXQZuZRzSv2Nh0PP23z_Pd38L0fzrBYmUkkL_9BkI5XbO8EVGi_8Q2ESPcoUNaKYwDQPhalWivFKnyEdJUsNOXBgxnAyVOGuJVbrNJfyLNZ0K_eBBbtpoQTx7JsBIwAGHSKmW3H_n6KzNsmwKf-vhEzhxt_qAM_qmsfXSMfxe2eRFAPxz-vBfDFpfJ3epxtmXdrgwSfbfSLFLWTFId5f5hsvh_DahCBMJZhKvvCuPva8UiS-jAlf5DKH49cp7w6D35Hy1LmLXj5VmOOHr1hhu6gG0Im8hQEqacSBoMjXtj98bl1l0e6M_JUcBcawFzeXnEMRcoghns-agy9mv809nTlXir9Kb3AwguviXIaRhyi7rW9AruxFUu5Iw");

// Demo both the legacy client and new SDK
AnsiConsole.MarkupLine("[bold blue]NucliaDB C# SDK Demo[/]");
AnsiConsole.WriteLine();

// Legacy client (for comparison) - simplified for demo
AnsiConsole.MarkupLine("[yellow]Legacy SynthaClient (deprecated):[/]");
#pragma warning disable CS0618 // Type or member is obsolete
var syntha = new SynthaClient(config);
#pragma warning restore CS0618 // Type or member is obsolete

AnsiConsole.MarkupLine("[dim]Legacy client streaming (first few chunks)...[/]");
var legacyCount = 0;
await foreach (var result in syntha.Ask("What is Progress Syntha?"))
{
    if (!string.IsNullOrEmpty(result.Data) && legacyCount < 10)
    {
        AnsiConsole.Write(result.Data.EscapeMarkup());
        legacyCount++;
    }
    if (legacyCount >= 10) break;
}
AnsiConsole.WriteLine("...");
AnsiConsole.MarkupLine("[yellow]✓[/] Legacy client test completed");
AnsiConsole.WriteLine();

AnsiConsole.Write(new Rule("[yellow bold underline]--done[/]").RuleStyle("grey").LeftJustified());

// New NucliaDB SDK
AnsiConsole.MarkupLine("[green]New NucliaDbClient SDK:[/]");
using var client = new NucliaDbClient(config);

try
{    // Test Knowledge Box info
    AnsiConsole.MarkupLine("[dim]Getting Knowledge Box information...[/]");
    var kbResult = await client.KnowledgeBoxes.GetKnowledgeBoxAsync(config.KnowledgeBaseId);
    if (kbResult.Success && kbResult.Data != null)
    {
        var title = kbResult.Data.Title ?? "Untitled";
        var uuid = kbResult.Data.Uuid ?? "Unknown";
        AnsiConsole.MarkupLine($"[green]✓[/] Knowledge Box: {title.EscapeMarkup()}");
        AnsiConsole.MarkupLine($"[dim]  UUID: {uuid.EscapeMarkup()}[/]");
    }
    else
    {
        var errorMsg = kbResult.Error ?? "Unknown error";
        AnsiConsole.MarkupLine($"[red]✗[/] Error: {errorMsg.EscapeMarkup()}");
    }

    // Test synchronous ask
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[dim]Testing synchronous ask...[/]");
    var askRequest = new AskRequest
    {
        Query = "What is Progress Syntha?",
        TopK = 5,
        Citations = true,
        Debug = false    };

    var askResult = await client.Search.AskAsync(askRequest);
    if (askResult.Success && askResult.Data != null)
    {
        var answerText = askResult.Data.Answer ?? "No answer";
        var displayText = answerText.Length > 150 ? answerText.Substring(0, 150) + "..." : answerText;
        AnsiConsole.MarkupLine($"[green]✓[/] Answer: {displayText.EscapeMarkup()}");
        AnsiConsole.MarkupLine($"[dim]  Status: {askResult.Data.Status ?? "unknown"}[/]");
        
        if (askResult.Data.RetrievalBestMatches != null)
        {
            AnsiConsole.MarkupLine($"[dim]  Sources: {askResult.Data.RetrievalBestMatches.Length} relevant results[/]");
        }    }
    else
    {
        var errorMsg = askResult.Error ?? "Unknown error";
        AnsiConsole.MarkupLine($"[red]✗[/] Error: {errorMsg.EscapeMarkup()}");
    }

    // Test catalog/search
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[dim]Searching resources catalog...[/]");
    var catalogResult = await client.Search.CatalogAsync("", pageSize: 5);    if (catalogResult.Success && catalogResult.Data != null)
    {
        AnsiConsole.MarkupLine($"[green]✓[/] Found {catalogResult.Data.Total} total resources");
        foreach (var resource in catalogResult.Data.ResourceList.Take(3))
        {
            var title = resource.Title ?? resource.Id ?? "Untitled";
            AnsiConsole.MarkupLine($"[dim]  - {title.EscapeMarkup()} (Status: {resource.Status})[/]");
        }
    }
    else
    {
        var errorMsg = catalogResult.Error ?? "Unknown error";
        AnsiConsole.MarkupLine($"[red]✗[/] Error: {errorMsg.EscapeMarkup()}");
    }

    // Test streaming (limited output)
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[dim]Testing streaming response (first few chunks)...[/]");
    var streamRequest = new AskRequest
    {
        Query = "Tell me about artificial intelligence",
        TopK = 3
    };

    AnsiConsole.Write("[dim]Stream: [/]");
    var streamCount = 0;
    await foreach (var response in client.Search.AskStreamAsync(streamRequest))
    {
        if (!string.IsNullOrEmpty(response.Data) && streamCount < 20)
        {
            AnsiConsole.Write(response.Data.EscapeMarkup());
            streamCount++;
        }
        
        if (streamCount >= 20) break; // Limit for demo
    }
    AnsiConsole.WriteLine("...");
    AnsiConsole.MarkupLine("[green]✓[/] Streaming completed");

    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[bold green]SDK Demo Complete![/]");
    AnsiConsole.MarkupLine("[dim]For comprehensive examples, see ProgressSyntha/Examples/SdkUsageExample.cs[/]");
    AnsiConsole.MarkupLine("[dim]For full documentation, see ProgressSyntha/README.md[/]");
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]❌ Unexpected error: {ex.Message.EscapeMarkup()}[/]");
}
