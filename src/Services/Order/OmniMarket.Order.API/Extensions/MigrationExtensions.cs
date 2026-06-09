using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using OmniMarket.Order.Infrastructure.Persistence;

namespace OmniMarket.Order.API.Extensions
{
	public static class MigrationExtensions
	{
		public static void ApplyMigrations(this IApplicationBuilder app)
		{
			using IServiceScope scope = app.ApplicationServices.CreateScope();
			using OrderDbContext context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();

			try
			{
				Console.WriteLine("[MIGRATION] Veritabanı ve tablolar kontrol ediliyor...");

				// En garanti kurumsal yöntem: Doğrudan Migrate çağrısı.
				// Bu komut DB yoksa oluşturur, tablo yoksa migration'ları sırayla basar.
				context.Database.Migrate();

				Console.WriteLine("[MIGRATION] Veritabanı yapısı başarıyla güncellendi!");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[MIGRATION ERROR] Tablolar basılırken hata oluştu: {ex.Message}");
				if (ex.InnerException != null)
				{
					Console.WriteLine($"[MIGRATION INNER ERROR] Detay: {ex.InnerException.Message}");
				}
				throw;
			}
		}
	}
}
