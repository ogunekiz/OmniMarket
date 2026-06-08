namespace OmniMarket.Catalog.Domain.Entities
{
	public class Category
	{
		public string Id { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
	}
}
