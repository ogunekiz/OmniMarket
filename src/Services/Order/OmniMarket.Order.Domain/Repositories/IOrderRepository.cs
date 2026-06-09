
namespace OmniMarket.Order.Domain.Repositories
{
	public interface IOrderRepository
	{
		Task<OmniMarket.Order.Domain.Entities.Order?> GetByIdAsync(int id);
		Task<IReadOnlyList<OmniMarket.Order.Domain.Entities.Order>> GetOrdersByBuyerIdAsync(string buyerId);
		Task AddAsync(OmniMarket.Order.Domain.Entities.Order order);
		void Update(OmniMarket.Order.Domain.Entities.Order order);
	}
}
