using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OmniMarket.Order.Application.Common;
using OmniMarket.Order.Application.Features.Orders.Consumers;
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

			// 🔥 3. MassTransit & RabbitMQ Altyapısı
			services.AddMassTransit(x =>
			{
				// 1. Tüketici sınıfımızı MassTransit'e kaydediyoruz
				x.AddConsumer<ProductPriceChangedConsumer>();

				x.UsingRabbitMq((context, cfg) =>
				{
					cfg.Host("omnimarket.rabbitmq", "/", h =>
					{
						h.Username("guest");
						h.Password("guest");
					});

					// 2. Kuyruk adını ve bu kuyruğu hangi Consumer'ın dinleyeceğini belirliyoruz
					cfg.ReceiveEndpoint("product-price-changed-queue", e =>
					{
						e.ConfigureConsumer<ProductPriceChangedConsumer>(context);
					});
				});
			});

			return services;
		}
	}
}
