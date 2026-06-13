using MediatR;
using OmniMarket.Order.Application.Common;
using OmniMarket.Order.Domain.Entities;
using OmniMarket.Order.Domain.Repositories;

namespace OmniMarket.Order.Application.Features.Products.Commands
{
	public record SyncProductCommand(string ProductId, string Name, decimal Price) : IRequest<bool>;

	public class SyncProductCommandHandler : IRequestHandler<SyncProductCommand, bool>
	{
		private readonly IProductRepository _productRepository;
		private readonly IUnitOfWork _unitOfWork;

		public SyncProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
		{
			_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}

		public async Task<bool> Handle(SyncProductCommand request, CancellationToken cancellationToken)
		{
			var product = await _productRepository.GetByIdAsync(request.ProductId);

			if (product != null)
			{
				product.Name = request.Name;
				product.Price = request.Price;

				_productRepository.Update(product);
			}
			else
			{
				var newProduct = new Product
				{
					Id = request.ProductId,
					Name = request.Name,
					Price = request.Price
				};
				await _productRepository.AddAsync(newProduct);
			}

			await _unitOfWork.SaveChangesAsync(cancellationToken);
			return true;
		}
	}
}
