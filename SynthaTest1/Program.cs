using ProgressSyntha;
using Spectre.Console;
using Spectre.Console.Json;
using System.Text.Json;

var config = new SynthaConfig(ZoneId: "progress-proc-us-east-2-1",
	KnowledgeBaseId: "886a82a2-b0d6-400d-9907-9b8c0567681a",
	ApiKey: "eyJhbGciOiJSUzI1NiIsImtpZCI6InNhIiwidHlwIjoiSldUIn0.eyJpc3MiOiJodHRwczovL3Byb2dyZXNzLXByb2MtdXMtZWFzdC0yLTEuc3ludGhhLnByb2dyZXNzLmNvbS8iLCJpYXQiOjE3NDQ5MDUwODAsInN1YiI6IjE3M2VhZWViLWIxZDctNDYxNi1iOTY2LTY1ODZhZTJhOGRiNiIsImp0aSI6IjA2ZmJmOTQxLWYzMGYtNGIyMC05YjBhLTFkZDJkZWQyZWJiNCIsImV4cCI6MTc3NjQ0MTA3OCwia2V5IjoiODY3YWYzYWUtZjQ0OC00NjQxLTk1ZmEtNzg0NmVmMjQ0YmFlIiwia2lkIjoiOTI2MTQwZGYtNTU5OS00MTVkLWI4YzYtYjdjYjM0YmZjZjYxIn0.b2K9V0y2DhGvSj8ZiraQFO3bG8fyPU68DrKaIfw9-HV2qBWbFXEq2DA689RfRHSvKwiUM8d3l8PEbB_G-SzgWYGQdXhTlN1iE3LbSiGaseVAPuKfqU2v6rXH3ayQ9D4QuZUTnXpJziM97WUntmdUsnNOwr3WQ8okLWYf5cGhuefQi7meZ1xAnpxW99vkQVA0gOh_JBhAZfDgESZQESYgpQr-Iq1u-YKObBbP4R7Y_l3ZcjpW4Ne5LXgKlfkhDmc8cRx-2tv1izA2zccyFlQzzWPrAoLBdrcuKVqlFlfd1LhuNIS8tTTXQZuZRzSv2Nh0PP23z_Pd38L0fzrBYmUkkL_9BkI5XbO8EVGi_8Q2ESPcoUNaKYwDQPhalWivFKnyEdJUsNOXBgxnAyVOGuJVbrNJfyLNZ0K_eBBbtpoQTx7JsBIwAGHSKmW3H_n6KzNsmwKf-vhEzhxt_qAM_qmsfXSMfxe2eRFAPxz-vBfDFpfJ3epxtmXdrgwSfbfSLFLWTFId5f5hsvh_DahCBMJZhKvvCuPva8UiS-jAlf5DKH49cp7w6D35Hy1LmLXj5VmOOHr1hhu6gG0Im8hQEqacSBoMjXtj98bl1l0e6M_JUcBcawFzeXnEMRcoghns-agy9mv809nTlXir9Kb3AwguviXIaRhyi7rW9AruxFUu5Iw");

var syntha = new SynthaClient(config);

var results = syntha.Ask("What is syntha");
Tree root = new("Response");
var acn = root.AddNode("Answer:");
string streamingText = "";
await AnsiConsole.Live(root)
	.StartAsync(async ctx =>
	{
		await foreach (var result in results)
		{
			switch (result.Item)
			{
				case AnswerContent answer:
					streamingText += answer.Text;
					root.Nodes.Remove(acn);
					acn = root.AddNode($"Answer: {streamingText}".EscapeMarkup());
					ctx.Refresh();
					break;

				case CitationsContent citation:
					foreach (var citationItem in citation.Citations)
					{
						var cite = root.AddNode($"Citation: {citationItem.Key}".EscapeMarkup());
						foreach (var item in citationItem.Value)
						{
							cite.AddNode($"{item}".EscapeMarkup());
						}
					}
					ctx.Refresh();
					break;

				case RetrievalContent retrieval when retrieval.Results is not null:
					var res = root.AddNode($"Resources");
					foreach (var resource in retrieval.Results.Resources)
					{
						//res.AddNode(resource.Key);
						var key = res.AddNode($"Thumbnail: {resource.Value.Thumbnail}".EscapeMarkup());
						foreach (var t in resource.Value.Data.Texts)
						{
							var body = t.Value?.Item?.Body;
							if (!string.IsNullOrWhiteSpace(body))
							{
								try
								{
									// Try to parse as JSON
									using var doc = JsonDocument.Parse(body);
									key.AddNode(new JsonText(body));
								}
								catch (JsonException)
								{
									key.AddNode(body.EscapeMarkup());
								}
								ctx.Refresh();
							}
						}
					}
					break;

				default:
					break;
			}
		}
	});

AnsiConsole.Write(new Rule("[yellow bold underline]--done[/]").RuleStyle("grey").LeftJustified());
