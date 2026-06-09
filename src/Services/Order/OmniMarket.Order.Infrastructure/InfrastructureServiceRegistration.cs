using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OmniMarket.Order.Application.Common;
using OmniMarket.Order.Domain.Repositories;
using OmniMarket.Order.Infrastructure.Persistence;
using OmniMarket.Order.Infrastructure.Repositories;

namespace OmniMarket.Order.Infrastructure
{
	public static class InfrastructureServiceRegistration
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			// 1. DbContext Kaydı
			services.AddDbContext<OrderDbContext>(options =>
					options.UseSqlServer(configuration.GetConnectionString("OrderConnectionString")));

			// 2. Repository ve UnitOfWork Kayıtları
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}
