using System.Text.Json;

public static class HttpClientExt
{
    public static async Task<T> ReadAsync<T>(this HttpClient client, string requestUri)
    {
        var response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        //return await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync())
            ?? throw new Exception("Failed to deserialize response.");
    }

    public static async Task<T> PostAsync<T>(this HttpClient client, string requestUri, T body)
    {
        var json = JsonSerializer.Serialize(body);
		var content = new StringContent(json, null, "application/json");
        var response = await client.PostAsync(requestUri, content);
        response.EnsureSuccessStatusCode();
        //return await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync())
            ?? throw new Exception("Failed to deserialize response.");
	}

	public static async Task<T> PutAsync<T>(this HttpClient client, string requestUri, T body)
	{
		var json = JsonSerializer.Serialize(body);
		var content = new StringContent(json, null, "application/json");
		var response = await client.PutAsync(requestUri, content);
		response.EnsureSuccessStatusCode();
		//return await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
		return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync())
			?? throw new Exception("Failed to deserialize response.");
	}
}