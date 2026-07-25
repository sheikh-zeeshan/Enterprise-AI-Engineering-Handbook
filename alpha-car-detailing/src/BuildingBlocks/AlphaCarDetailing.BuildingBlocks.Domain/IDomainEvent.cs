namespace AlphaCarDetailing.BuildingBlocks.Domain;

public interface IDomainEvent
{
    DateTimeOffset OccurredOnUtc { get; }
}
