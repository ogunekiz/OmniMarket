namespace OmniMarket.Catalog.Application.Features.Products.Queries
{
	public record GetProductsDto(
			string Id,
			string Name,
			string Description,
			decimal Price,
			int Stock,
			string CategoryId);
}