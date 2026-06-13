using Microsoft.EntityFrameworkCore;
using OmniMarket.Order.Domain.Entities;
using OmniMarket.Order.Domain.Repositories;
using OmniMarket.Order.Infrastructure.Persistence;

namespace OmniMarket.Order.Infrastructure.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly OrderDbContext _context;

		public ProductRepository(OrderDbContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task AddAsync(Product product)
		{
			await _context.Products.AddAsync(product);
		}

		public async Task<Product?> GetByIdAsync(string id)
		{
			return await _context.Products.FirstOrDefaultAsync(o => o.Id == id);
		}

		public void Update(Product product)
		{
			_context.Entry(product).State = EntityState.Modified;
		}
	}
}
