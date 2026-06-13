namespace OmniMarket.Shared
{
	public record ProductPriceChangedEvent
	{
		public string ProductId { get; init; } = string.Empty;
		public decimal NewPrice { get; init; }
		public string Name { get; init; } = string.Empty;
		public DateTime ChangedDate { get; init; } = DateTime.UtcNow;
	}
}
