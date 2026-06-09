using Microsoft.EntityFrameworkCore;
using OmniMarket.Order.Domain.Repositories;
using OmniMarket.Order.Infrastructure.Persistence;
using OrderEntity = OmniMarket.Order.Domain.Entities.Order; // Çakışmayı önlemek için alias

namespace OmniMarket.Order.Infrastructure.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly OrderDbContext _context;

		public OrderRepository(OrderDbContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<OrderEntity?> GetByIdAsync(int id)
		{
			// Eager Loading kullanarak Sipariş ile birlikte kalemlerini de çekiyoruz
			return await _context.Orders
					.Include(o => o.OrderItems)
					.FirstOrDefaultAsync(o => o.Id == id);
		}

		public async Task<IReadOnlyList<OrderEntity>> GetOrdersByBuyerIdAsync(string buyerId)
		{
			return await _context.Orders
					.Include(o => o.OrderItems)
					.Where(o => o.BuyerId == buyerId)
					.ToListAsync();
		}

		public async Task AddAsync(OrderEntity order)
		{
			await _context.Orders.AddAsync(order);
		}

		public void Update(OrderEntity order)
		{
			// EF Core zaten nesneyi takip (track) ettiği için State değişikliği yapmak yeterlidir
			_context.Entry(order).State = EntityState.Modified;
		}
	}
}
