using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OmniMarket.Order.Application
{
	public static class ApplicationServiceRegistration
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			// Bu assembly içindeki tüm MediatR Handler'larını otomatik olarak IoC Container'a kaydeder
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

			return services;
		}
	}
}
