using ProgressSyntha;
using System.Net.Http.Json;
using System.Text.Json;

var syntha = new Syntha(new HttpClient());

var results = syntha.Ask("What is syntha");

await foreach (var result in results)
{
	if (result.Item?.Type == "answer")
	{
		Console.Write(result?.Item?.Text);
	}
	if (result?.Item?.Type == "retrieval" && result.Item is not null)
	{
		Console.WriteLine();
		Console.WriteLine("--- Resources ---");
		if (result.Item is { Type: "retrieval" } retrievalItem && retrievalItem.Results is not null)
		{
			foreach (var resource in retrievalItem.Results.Resources)
			{
				Console.WriteLine($"|- {resource.Key}");
				Console.WriteLine($"|- Thumbnail: {resource.Value.Thumbnail }");
				foreach(var t in resource.Value.Data.Texts)
				Console.WriteLine($"|- Meta: {t.Value.Item.Body}");
			}
		}
	}
}
Console.WriteLine("done");
Console.ReadLine();
