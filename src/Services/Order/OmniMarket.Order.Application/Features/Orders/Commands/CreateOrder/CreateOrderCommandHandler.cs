using MediatR;
using OmniMarket.Order.Application.Common;
using OmniMarket.Order.Domain.Repositories;
using OrderEntity = OmniMarket.Order.Domain.Entities.Order; // İsim çakışması önleyici alias

namespace OmniMarket.Order.Application.Features.Orders.Commands.CreateOrder
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
		{
			_orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}

		public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			// 1. DDD Kuralı: Yeni bir Sipariş nesnesi (Aggregate Root) oluşturuyoruz
			var newOrder = new OrderEntity(request.BuyerId);

			// 2. Rich Domain Model: İş kurallarını işleterek kalemleri siparişe ekliyoruz
			foreach (var item in request.OrderItems)
			{
				newOrder.AddOrderItem(item.ProductId, item.ProductName, item.Price, item.Quantity);
			}

			// 3. Altyapı Katmanına Bildirim: Veritabanı takibine alıyoruz
			await _orderRepository.AddAsync(newOrder);

			// 4. Unit of Work: Tüm işlemleri tek bir Transaction olarak MSSQL'e kaydediyoruz
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			// Geriye oluşturulan siparişin otomatik artan ID'sini dönüyoruz
			return newOrder.Id;
		}
	}
}
