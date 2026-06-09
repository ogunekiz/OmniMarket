using OmniMarket.Order.Domain.Common;

namespace OmniMarket.Order.Domain.Entities
{
	public class OrderItem : Entity
	{
		public string ProductId { get; private set; }
		public string ProductName { get; private set; }
		public decimal Price { get; private set; }
		public int Quantity { get; private set; }

		// EF Core için boş constructor
		private OrderItem() { }

		public OrderItem(string productId, string productName, decimal price, int quantity)
		{
			ProductId = productId;
			ProductName = productName;
			Price = price;
			Quantity = quantity;
		}
	}
}
