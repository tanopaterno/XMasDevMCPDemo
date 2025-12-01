using Api.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrdersController : ControllerBase
	{
		private static List<Order> Orders = new List<Order>();

		[HttpGet]
		public IActionResult GetOrders()
		{
			return Ok(Orders);
		}

		[HttpPost]
		public IActionResult PostOrder([FromBody] Order order)
		{
			order.Id = Guid.NewGuid();
			Orders.Add(order);
			return Ok(order);
		}

		[HttpPut]
		public IActionResult PutOrder([FromBody] Order order)
		{
			var currentOrder = Orders.FirstOrDefault(x => x.Id == order.Id);
			currentOrder.Toy = order.Toy;
			currentOrder.Material = order.Material;
			currentOrder.Quantity = order.Quantity;
			return Ok(currentOrder);
		}

		[HttpDelete]
		public IActionResult DeleteOrder(Guid id)
		{
			Orders = Orders.Where(x => x.Id != id).ToList();
			return Ok();
		}
	}
}
