using Api.Shared.Models;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace Server.Console
{
	[McpServerToolType]
	public static class KidTools
	{
		[McpServerTool, Description("Get list of kids.")]
		public static async Task<string> GetKids(HttpClient client)
		{
			var kid = await client.ReadAsync<List<Kid>>($"/Kids");
			if (!kid.Any())
			{
				return "Kid list is empty.";
			}

			return string.Join("\n--\n", kid
				.Select(p =>
				{
					return $"""
                        Id: {p.Id} 
                        Name: {p.Name}
                        Surname: {p.Surname}
                        Age: {p.Age}
                        Required toy: {p.Toy}
                        """;
				}));
		}

		[McpServerTool, Description("Create a new kid with name, surname and date of birthday.")]
		public static async Task<string> PostKid(
			HttpClient client,
			[Description("The name of kid")] string name,
			[Description("The surname of kid")] string surname,
			[Description("The age of birthday of kid")] int? age,
			[Description("The required toy of kid")] string toy)
		{
			var newKid = new Kid
			{
				Name = name,
				Surname = surname,
				Age = age,
				Toy = toy,
			};

			var response = await client.PostAsync<Kid>($"/Kids", newKid);
			if (response is null)
			{
				return "Kid not created.";
			}

			return string.Join("\n--\n",
				$"""
                Message: "Kid {response.Name} {response.Surname}, age {response.Age}, required toy {response.Toy}, successful created."
                """
			);
		}

		[McpServerTool, Description("Update name, surname or date of birthday of kid.")]
		public static async Task<string> PutKid(
			HttpClient client,
			[Description("The id of kid")] Guid id,
			[Description("The name of kid")] string name,
			[Description("The surname of kid")] string surname,
			[Description("The age of birthday of kid")] int? age,
			[Description("The required toy of kid")] string toy)
		{
			var existKid = new Kid
			{
				Id = id,
				Name = name,
				Surname = surname,
				Age = age,
				Toy = toy,
			};

			var response = await client.PutAsync<Kid>($"/Kids", existKid);
			if (response is null)
			{
				return "Kid not created.";
			}

			return string.Join("\n--\n",
				$"""
                Message: "Kid {response.Name} {response.Surname}, age {response.Age}, required toy {response.Toy}, successful updated."
                """
			);
		}
	}
}
