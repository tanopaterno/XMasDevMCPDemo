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
				DateOfBirthday = new DateTime(2004, 5, 31),
				Toy = "Peluche",
			},
			new Kid
			{
				Id = Guid.NewGuid(),
				Name = "Bugs",
				Surname = "Bunny",
				DateOfBirthday = new DateTime(1938, 4, 30),
				Toy = "Carote",
			},
			new Kid
			{
				Id = Guid.NewGuid(),
				Name = "Homer",
				Surname = "Simpson",
				DateOfBirthday = new DateTime(1987, 4, 19),
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
			currentPatient.DateOfBirthday = kid.DateOfBirthday;
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
