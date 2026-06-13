using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OmniMarket.Catalog.Application.Configurations;
using OmniMarket.Catalog.Application.Interfaces;
using OmniMarket.Catalog.Infrastructure.Repositories;

namespace OmniMarket.Catalog.Infrastructure
{
	public static class InfrastructureDependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			// appsettings.json içerisindeki DatabaseSettings alanını nesneye bağlıyoruz
			services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));

			services.AddScoped<IProductRepository, ProductRepository>();

			// 🔥 MassTransit & RabbitMQ Altyapısı (Publisher olarak)
			services.AddMassTransit(x =>
			{
				x.UsingRabbitMq((context, cfg) =>
				{
					// Ortak Docker ağındaki RabbitMQ konteyner ismi ve iç portu
					cfg.Host("omnimarket.rabbitmq", "/", h =>
					{
						h.Username("guest");
						h.Password("guest");
					});
				});
			});

			return services;
		}
	}
}
