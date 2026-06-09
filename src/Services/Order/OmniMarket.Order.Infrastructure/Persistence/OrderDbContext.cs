using Microsoft.EntityFrameworkCore;
using OmniMarket.Order.Domain.Entities;

namespace OmniMarket.Order.Infrastructure.Persistence
{
	public class OrderDbContext : DbContext
	{
		public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
		{
		}

		public DbSet<Domain.Entities.Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Fluent API konfigürasyonları
			modelBuilder.Entity<Domain.Entities.Order>(entity =>
			{
				entity.ToTable("Orders");
				entity.HasKey(o => o.Id);

				// Rich Domain Model'deki private readonly listeyi EF Core'a tanıtıyoruz
				var navigation = modelBuilder.Entity<Domain.Entities.Order>()
						.Metadata.FindNavigation(nameof(Domain.Entities.Order.OrderItems));

				navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

				// Order ile OrderItem arasındaki 1-to-Many ilişki
				entity.HasMany(o => o.OrderItems)
							.WithOne()
							.HasForeignKey("OrderId") // Shadow Property olarak arkada tutulacak
							.OnDelete(DeleteBehavior.Cascade); // Sipariş silinirse kalemleri de silinsin
			});

			modelBuilder.Entity<OrderItem>(entity =>
			{
				entity.ToTable("OrderItems");
				entity.HasKey(oi => oi.Id);
				entity.Property(oi => oi.Price).HasColumnType("decimal(18,2)");
			});

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Domain.Entities.Order>()
				.Property(o => o.TotalPrice)
				.HasColumnType("decimal(18,2)");

		}
	}
}
