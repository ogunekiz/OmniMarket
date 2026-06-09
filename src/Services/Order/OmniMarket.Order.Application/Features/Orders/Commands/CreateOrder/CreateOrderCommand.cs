using MediatR;

namespace OmniMarket.Order.Application.Features.Orders.Commands.CreateOrder
{
	// MediatR'a bu komutun sonucunda geriye "int" (yani siparişin ID'sini) döneceğimizi söylüyoruz
	public record CreateOrderCommand : IRequest<int>
	{
		public string BuyerId { get; init; } = string.Empty;
		public List<OrderItemDto> OrderItems { get; init; } = new();
	}

	public record OrderItemDto
	{
		public string ProductId { get; init; } = string.Empty;
		public string ProductName { get; init; } = string.Empty;
		public decimal Price { get; init; }
		public int Quantity { get; init; }
	}
}
