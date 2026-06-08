namespace OmniMarket.Catalog.Domain.Entities
{
	public class Product
	{
		public string Id { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public int Stock { get; set; }
		public string CategoryId { get; set; } = string.Empty;
		public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
	}
}
