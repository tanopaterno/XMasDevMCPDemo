using Api.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class KidsController : ControllerBase
	{
		private static List<Kid> Kids = new List<Kid>
		{
			new Kid
			{
				Id = Guid.NewGuid(),
				Name = "Peppa",
				Surname = "Pig",
				Age = 21,
				Toy = "Peluche",
			},
			new Kid
			{
				Id = Guid.NewGuid(),
				Name = "Bugs",
				Surname = "Bunny",
				Age = 87,
				Toy = "Carote",
			},
			new Kid
			{
				Id = Guid.NewGuid(),
				Name = "Homer",
				Surname = "Simpson",
				Age = 38,
				Toy = "Birre",
			},
		};

		[HttpGet]
		public IActionResult GetKids()
		{
			return Ok(Kids);
		}

		[HttpPost]
		public IActionResult PostKid([FromBody] Kid kid)
		{
			kid.Id = Guid.NewGuid();
			Kids.Add(kid);
			return Ok(kid);
		}

		[HttpPut]
		public IActionResult PutKid([FromBody] Kid kid)
		{
			var currentPatient = Kids.FirstOrDefault(x => x.Id == kid.Id);
			currentPatient.Name = kid.Name;
			currentPatient.Surname = kid.Surname;
			currentPatient.Age = kid.Age;
			currentPatient.Toy = kid.Toy;
			return Ok(currentPatient);
		}

		[HttpDelete]
		public IActionResult DeleteKid(Guid id)
		{
			Kids = Kids.Where(x => x.Id != id).ToList();
			return Ok();
		}
	}
}
