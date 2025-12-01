using Api.Shared.Models;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace Server.Console
{
	[McpServerToolType]
	public static class OrderTools
	{
		[McpServerTool, Description("Get list of orders.")]
		public static async Task<string> GetOrders(HttpClient client)
		{
			var kid = await client.ReadAsync<List<Order>>($"/Orders");
			if (!kid.Any())
			{
				return "Order list is empty.";
			}

			return string.Join("\n--\n", kid
				.Select(p =>
				{
					return $"""
                        Id: {p.Id} 
                        Toy: {p.Toy}
                        Material: {p.Material}
                        Quantity: {p.Quantity}
                        """;
				}));
		}

		[McpServerTool, Description("Create a new order with toy name, material for make the toy and quantity.")]
		public static async Task<string> PostOrder(
			HttpClient client,
			[Description("The name of toy")] string toy,
			[Description("The material for make the toy")] string material,
			[Description("The quantity of material for make the toy")] int quantity)
		{
			var newOrder = new Order
			{
				Toy = toy,
				Material = material,
				Quantity = quantity,
			};

			var response = await client.PostAsync<Order>($"/Orders", newOrder);
			if (response is null)
			{
				return "Order not created.";
			}

			return string.Join("\n--\n",
				$"""
                Message: "Order of {response.Quantity} {response.Material} for make the {response.Toy} successful created."
                """
			);
		}

		[McpServerTool, Description("Update toy name, material or quantity of exist order.")]
		public static async Task<string> PutOrder(
			HttpClient client,
			[Description("The id of order")] Guid id,
			[Description("The name of toy")] string toy,
			[Description("The material for make the toy")] string material,
			[Description("The quantity of material for make the toy")] int quantity)
		{
			var existOrder = new Order
			{
				Id = id,
				Toy = toy,
				Material = material,
				Quantity = quantity,
			};

			var response = await client.PutAsync<Order>($"/Orders", existOrder);
			if (response is null)
			{
				return "Order not updated.";
			}

			return string.Join("\n--\n",
				$"""
                Message: "Order of {response.Quantity} {response.Material} for make the {response.Toy} successful updated."
                """
			);
		}
	}
}
