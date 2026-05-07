namespace Blog.Domain.Common;

public interface IDomainEvent
{
    Guid EventId { get; }
    DateTime OccurredOnUtc { get; }
    Guid? CorrelationId { get; }
}
