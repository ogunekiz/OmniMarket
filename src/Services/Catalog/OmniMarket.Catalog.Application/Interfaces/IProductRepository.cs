using OmniMarket.Catalog.Domain.Entities;

namespace OmniMarket.Catalog.Application.Interfaces
{
	public interface IProductRepository
	{
		Task<List<Product>> GetAllAsync();
		Task CreateAsync(Product product);
	}
}
