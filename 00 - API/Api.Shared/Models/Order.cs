using System.Text.Json.Serialization;

namespace Api.Shared.Models
{
	public class Order
	{

		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("toy")]
		public string Toy { get; set; }

		[JsonPropertyName("material")]
		public string Material { get; set; }

		[JsonPropertyName("quantity")]
		public int Quantity { get; set; }
	}
}
