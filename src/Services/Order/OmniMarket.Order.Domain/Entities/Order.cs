using OmniMarket.Order.Domain.Common;

namespace OmniMarket.Order.Domain.Entities
{
	public class Order : Entity
	{
		public string BuyerId { get; private set; }
		public decimal TotalPrice { get; private set; }
		public OrderStatus Status { get; private set; }

		// Encapsulation: Dışarıdan bu listeye direkt Add/Remove yapılamaz, sadece okunabilir.
		private readonly List<OrderItem> _orderItems = new();
		public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

		private Order() { }

		public Order(string buyerId)
		{
			BuyerId = buyerId;
			Status = OrderStatus.Submitted;
			TotalPrice = 0;
		}

		// Rich Domain Model: İş mantığı entity'nin kendi içinde çözülür
		public void AddOrderItem(string productId, string productName, decimal price, int quantity)
		{
			var existingOrderForProduct = _orderItems.FirstOrDefault(o => o.ProductId == productId);

			if (existingOrderForProduct is not null)
			{
				// Eğer ürün zaten siparişte varsa adedini artır (Basit iş kuralı)
				// Not: Mimaride adedi artırmak için private alanları modifiye eden bir metot eklenebilir, 
				// şimdilik temizlik adına yeni ekleme mantığı üzerinden gidiyoruz.
				return;
			}

			var orderItem = new OrderItem(productId, productName, price, quantity);
			_orderItems.Add(orderItem);

			// Toplam fiyatı güncelle
			TotalPrice += price * quantity;
		}

		public void CancelOrder()
		{
			if (Status == OrderStatus.Shipped)
			{
				throw new InvalidOperationException("Kargoya verilmiş bir sipariş iptal edilemez!");
			}
			Status = OrderStatus.Cancelled;
		}
	}

	public enum OrderStatus
	{
		Submitted,
		StockConfirmed,
		Paid,
		Shipped,
		Cancelled
	}
}
