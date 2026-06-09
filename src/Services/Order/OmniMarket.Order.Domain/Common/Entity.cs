namespace OmniMarket.Order.Domain.Common
{
	public abstract class Entity
	{
		public int Id { get; protected set; }
		public DateTime CreatedDate { get; protected set; } = DateTime.UtcNow;
	}
}
