using Microsoft.JSInterop;

namespace Syntha.BlazorWeb.Components.Pages
{
	public class BlobInterop(IJSRuntime JSRuntime)
	{
		public async Task SetSource(string elementId, byte[] stream, string contentType, string title)
		{

			// Pass the byte array to JSInterop
			await JSRuntime.InvokeVoidAsync("setSource", elementId, stream, contentType, title);
		}
	}
}	
