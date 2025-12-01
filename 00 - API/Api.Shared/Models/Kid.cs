using System.Text.Json.Serialization;

namespace Api.Shared.Models
{
	public class Kid
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("surname")]
		public string Surname { get; set; }

		[JsonPropertyName("age")]
		public int? Age { get; set; }

		[JsonPropertyName("toy")]
		public string Toy { get; set; }

		[JsonPropertyName("note")]
		public string? Note { get; set; }
	}
}
