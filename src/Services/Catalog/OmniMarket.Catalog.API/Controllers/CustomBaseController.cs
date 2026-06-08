using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OmniMarket.Catalog.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomBaseController : ControllerBase
	{

		private IMediator? _mediator;

		// Eğer miras alan controller'da mediator çağrılırsa ve null ise, IoC Container'dan çözümlüyoruz.
		// Bu sayede her controller constructor'ında tek tek injection yapmaktan kurtuluyoruz.
		protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

	}
}
