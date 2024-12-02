namespace WebLLM.Pages;

using Microsoft.JSInterop;
public class WebLLMService
{
    private string model = "Llama-3.2-1B-Instruct-q4f16_1-MLC";
	private readonly Lazy<Task<IJSObjectReference>> moduleTask;
	
	//private const string ModulePath = "https://edcharbeneau.com/javascript/webllm-interop.js";
	private const string ModulePath = "./webllm-interop.js";
	public WebLLMService(IJSRuntime jsRuntime)
	{
		moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
		"import", $"{ModulePath}").AsTask());
	}

	public async Task Initialize()
	{
		var module = await moduleTask.Value;
		await module.InvokeVoidAsync("initialize", model, DotNetObjectReference.Create(this));
	}

	public event Action<InitProgress>? OnInitializingChanged;

	[JSInvokable]
	public Task OnInitializing(InitProgress status)
	{
		OnInitializingChanged?.Invoke(status);
		return Task.CompletedTask;
	}

	public async Task<WebLLMCompletion> CompleteAsync(IList<Message> messages)
	{
		var module = await moduleTask.Value;
		return await module.InvokeAsync<WebLLMCompletion>("complete", messages);
	}

}

