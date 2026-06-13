using MassTransit;
using MediatR;
using OmniMarket.Order.Application.Features.Products.Commands;
using OmniMarket.Shared;

namespace OmniMarket.Order.Application.Features.Orders.Consumers
{
	public class ProductPriceChangedConsumer : IConsumer<ProductPriceChangedEvent>
	{
		private readonly IMediator _mediator;

		public ProductPriceChangedConsumer(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task Consume(ConsumeContext<ProductPriceChangedEvent> context)
		{
			var message = context.Message;

			await _mediator.Send(new SyncProductCommand(message.ProductId, message.Name, message.NewPrice));
		}
	}
}
