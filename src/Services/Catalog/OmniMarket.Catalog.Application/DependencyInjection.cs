using Microsoft.Extensions.DependencyInjection;

namespace OmniMarket.Catalog.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			// Bu kod, Application katmanındaki tüm MediatR Handler'larını otomatik olarak tarar ve kaydeder.
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

			return services;
		}
	}
}
