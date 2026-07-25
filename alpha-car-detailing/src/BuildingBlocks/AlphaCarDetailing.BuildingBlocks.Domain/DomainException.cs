namespace AlphaCarDetailing.BuildingBlocks.Domain;

public sealed class DomainException(string message) : Exception(message);
