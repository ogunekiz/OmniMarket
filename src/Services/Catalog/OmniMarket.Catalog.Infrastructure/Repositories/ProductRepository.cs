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
