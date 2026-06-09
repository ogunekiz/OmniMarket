using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OmniMarket.Catalog.Application.Configurations;
using OmniMarket.Catalog.Application.Interfaces;
using OmniMarket.Catalog.Domain.Entities;

namespace OmniMarket.Catalog.Infrastructure.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly IMongoCollection<Product> _productCollection;

		public ProductRepository(IOptions<DatabaseSettings> databaseSettings)
		{
			// MongoDB istemcisi (Client) oluşturuluyor
			var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

			// Veritabanı alınıyor
			var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

			// Tablo (Collection) alınıyor
			_productCollection = mongoDatabase.GetCollection<Product>(databaseSettings.Value.CollectionName);

			SeedData();

		}

		private void SeedData()
		{
			// Koleksiyonda kayıt var mı diye bakıyoruz
			bool existsProduct = _productCollection.Find(x => true).Any();

			if (!existsProduct)
			{
				var seedProducts = new List<Product>
				{
						new Product
						{
								Id = "602d2149e773f2a3990b47f5", // Standart 24 karakterli hex Id
                Name = "IPhone 15 Pro",
								Description = "Titanyum kasa, A17 Pro işlemci ve harika kamera sistemi.",
								Price = 75000,
								Stock = 50,
								CategoryId = "602d2149e773f2a3990b47a1", // Örnek Kategori Id
                CreatedDate = DateTime.UtcNow
						},
						new Product
						{
								Id = "602d2149e773f2a3990b47f6",
								Name = "Samsung Galaxy S24 Ultra",
								Description = "Galaxy AI özellikleri ve 200 MP kamera deneyimi.",
								Price = 68000,
								Stock = 35,
								CategoryId = "602d2149e773f2a3990b47a1", // Örnek Kategori Id
                CreatedDate = DateTime.UtcNow
						},
						new Product
						{
								Id = "602d2149e773f2a3990b47f7",
								Name = "MacBook Pro 16 M3",
								Description = "M3 Max işlemci, 32GB RAM, 1TB SSD profesyonel bilgisayar.",
								Price = 120000,
								Stock = 15,
								CategoryId = "602d2149e773f2a3990b47a2", // Bilgisayar Kategorisi Id
                CreatedDate = DateTime.UtcNow
						}
				};

				// Verileri MongoDB koleksiyonuna nizamıyla basıyoruz
				_productCollection.InsertMany(seedProducts);
			}
		}

		public async Task<List<Product>> GetAllAsync()
		{
			return await _productCollection.Find(x => true).ToListAsync();
		}


		public async Task CreateAsync(Product product)
		{
			await _productCollection.InsertOneAsync(product);
		}

	}
}
