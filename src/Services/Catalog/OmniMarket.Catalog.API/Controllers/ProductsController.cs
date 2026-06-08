using Microsoft.AspNetCore.Mvc;
using OmniMarket.Catalog.Application.Features.Products.Commands;
using OmniMarket.Catalog.Application.Features.Products.Queries;

namespace OmniMarket.Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : CustomBaseController
	{
		// GET api/products
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var query = new GetAllProductsQuery();
			var response = await Mediator.Send(query);
			return Ok(response);
		}

		// POST api/products
		[HttpPost]
		public async Task<IActionResult> Create(CreateProductCommand command)
		{
			var response = await Mediator.Send(command);
			return Ok(new { Id = response, Message = "Ürün başarıyla eklendi" });
		}
	}
}
