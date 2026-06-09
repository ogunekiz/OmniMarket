using MediatR;
using Microsoft.AspNetCore.Mvc;
using OmniMarket.Order.Application.Features.Orders.Commands.CreateOrder;
using System.Net;

namespace OmniMarket.Order.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public OrdersController(IMediator mediator)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderCommand command)
		{
			// Komutu doğrudan MediatR üzerinden Handler'ına gönderiyoruz
			var result = await _mediator.Send(command);
			return Ok(result);
		}
	}
}
