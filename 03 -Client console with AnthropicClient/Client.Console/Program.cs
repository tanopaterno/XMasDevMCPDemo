using Anthropic.SDK;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Client;

#region Basic Client Structure

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
	.AddEnvironmentVariables()
	.AddUserSecrets<Program>();

var clientTransport = new StdioClientTransport(new()
{
	Name = "XmasDev Demo Server",
	Command = @"C:\Users\Demo\source\repos\XMasDevMCPDemo\01 - Server console\Server.Console\bin\Debug\net9.0\Server.Console.exe",
	Arguments = [],
});

await using var mcpClient = await McpClient.CreateAsync(clientTransport);

var tools = await mcpClient.ListToolsAsync();
foreach (var tool in tools)
{
	Console.WriteLine($"Connected to server with tools: {tool.Name}");
}

#endregion

#region Query processing logic

using var anthropicClient = new AnthropicClient(new APIAuthentication(builder.Configuration["ANTHROPIC_API_KEY"]))
	.Messages
	.AsBuilder()
	.UseFunctionInvocation()
	.Build();

var options = new ChatOptions
{
	MaxOutputTokens = 1000,
	ModelId = "claude-sonnet-4-5-20250929",
	Tools = [.. tools]
};

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

	await foreach (var message in anthropicClient.GetStreamingResponseAsync(query, options))
	{
		Console.Write(message);
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

#endregion
