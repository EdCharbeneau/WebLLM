namespace WebLLM.Pages;
public class InitProgress
{
	public float? Progress { get; set; }
	public string Text { get; set; } = "";
}

public class WebLLMCompletion
{
	public string? Id { get; set; }
	public string? Object { get; set; }
	public string? Model { get; set; }
	public string? SystemFingerprint { get; set; }
	public Choice[]? Choices { get; set; }
}

public class Choice
{
	public int? Index { get; set; }
	public Message? Message { get; set; }
	public object? Logprobs { get; set; }
	public string? FinishReason { get; set; }
}

public class Message
{
	public string Role { get; set; } = "user";
	public string Content { get; set; } = "";
}


