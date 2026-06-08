using MediatR;
using OmniMarket.Catalog.Application.Interfaces;
using OmniMarket.Catalog.Domain.Entities;

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

		public CreateProductCommandHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
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
			return product.Id;
		}
	}

}
