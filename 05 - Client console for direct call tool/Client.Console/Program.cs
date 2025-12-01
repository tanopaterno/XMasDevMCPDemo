using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
	.AddEnvironmentVariables()
	.AddUserSecrets<Program>();

var clientTransport = new StdioClientTransport(new()
{
	Name = "Demo Server",
	Command = @"C:\Users\Demo\source\repos\XMasDevMCPDemo\01 - Server console\Server.Console\bin\Debug\net9.0\Server.Console.exe",
	Arguments = [],
});

Console.WriteLine("Setting up stdio transport");

await using var mcpClient = await McpClientFactory.CreateAsync(clientTransport);

Console.WriteLine("Listing tools:");

var tools = await mcpClient.ListToolsAsync();

foreach (var tool in tools)
{
	Console.WriteLine($"Connected to server with tools: {tool.Name}");
}

var result = await mcpClient.CallToolAsync("get_kids", cancellationToken: CancellationToken.None);

Console.WriteLine("Result: " + Environment.NewLine + ((TextContentBlock)result.Content[0]).Text);

Console.ReadLine();
