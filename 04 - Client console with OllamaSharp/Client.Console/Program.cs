// set up the client
using OllamaSharp;
using OllamaSharp.ModelContextProtocol;
using System.Text.Json;

var uri = new Uri("http://localhost:11434");
var ollama = new OllamaApiClient(uri);

//var models = await ollama.ListLocalModelsAsync();

// select a model which should be used for further operations
//ollama.SelectedModel = "gemma3:4b"; //does not support tools
ollama.SelectedModel = "llama3.1:8b";


var chat = new Chat(ollama);
var tools = await Tools.GetFromMcpServers(".\\config.json");
var toolsJson = JsonSerializer.Serialize(tools);

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("MCP Client Started!");
Console.ResetColor();

PromptForInput();

while (Console.ReadLine() is string query && !"exit".Equals(query, StringComparison.OrdinalIgnoreCase))
{
	if (string.IsNullOrWhiteSpace(query))
	{
		PromptForInput();
		continue;
	}

	await foreach (var answerToken in chat.SendAsync(query, tools))
	{
		Console.Write(answerToken);
	}

	Console.WriteLine();

	PromptForInput();
}

static void PromptForInput()
{
	Console.WriteLine("Enter a command (or 'exit' to quit):");
	Console.ForegroundColor = ConsoleColor.Cyan;
	Console.Write("> ");
	Console.ResetColor();
}