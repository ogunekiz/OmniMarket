using OmniMarket.Order.Domain.Entities;

namespace OmniMarket.Order.Domain.Repositories
{
	public interface IProductRepository
	{
		Task<Product?> GetByIdAsync(string id);
		Task AddAsync(Product product);
		void Update(Product product);
	}
}
