using MediatR;
using OmniMarket.Catalog.Application.Interfaces;

namespace OmniMarket.Catalog.Application.Features.Products.Queries
{
	public record GetAllProductsQuery() : IRequest<List<GetProductsDto>>;

	public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<GetProductsDto>>
	{
		private readonly IProductRepository _productRepository;

		public GetAllProductsQueryHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<List<GetProductsDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
		{
			var products = await _productRepository.GetAllAsync();

			return products.Select(p => new GetProductsDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId)).ToList();
		}
	}

}
