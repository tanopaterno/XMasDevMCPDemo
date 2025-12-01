using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

//builder.Logging.ClearProviders();

builder.Services.AddMcpServer()
	.WithHttpTransport()
	.WithToolsFromAssembly();

builder.Services.AddSingleton(_ =>
{
	var client = new HttpClient() { BaseAddress = new Uri("http://localhost:5267") };
	//client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("api-tool", "1.0"));
	return client;
});

builder.Services.AddOpenApi();

builder.Services.AddCors(opt =>
{
	opt.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyHeader()
			.AllowAnyMethod()
			.SetIsOriginAllowed(_ => true)
			.AllowCredentials()
			.WithExposedHeaders(HeaderNames.ContentDisposition);
	});
});

// Add services to the container.

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();

app.MapOpenApi();

app.UseSwaggerUI(opt =>
{
	opt.SwaggerEndpoint("/openapi/v1.json", "v1");
});

app.MapMcp();

// Configure the HTTP request pipeline.

app.Run();
