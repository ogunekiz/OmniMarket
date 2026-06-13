using MassTransit;
using MediatR;
using OmniMarket.Catalog.Application.Interfaces;
using OmniMarket.Catalog.Domain.Entities;
using OmniMarket.Shared;

namespace OmniMarket.Catalog.Application.Features.Products.Commands
{
	public record CreateProductCommand(
			string Name,
			string Description,
			decimal Price,
			int Stock,
			string CategoryId) : IRequest<string>;

	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
	{
		private readonly IProductRepository _productRepository;
		private readonly IPublishEndpoint _publishEndpoint;

		public CreateProductCommandHandler(IProductRepository productRepository, IPublishEndpoint publishEndpoint)
		{
			_productRepository = productRepository;
			_publishEndpoint = publishEndpoint;
		}

		public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			var product = new Product
			{
				Id = Guid.NewGuid().ToString(),
				Name = request.Name,
				Description = request.Description,
				Price = request.Price,
				Stock = request.Stock,
				CategoryId = request.CategoryId
			};

			await _productRepository.CreateAsync(product);

			await _publishEndpoint.Publish(new ProductPriceChangedEvent
			{
				ProductId = product.Id,
				Name = product.Name,
				NewPrice = product.Price,
				ChangedDate = DateTime.UtcNow
			}, cancellationToken);

			return product.Id;
		}
	}

}
