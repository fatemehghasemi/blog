namespace Blog.Domain.Common;

public abstract class BaseDomainEvent : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
    public Guid? CorrelationId { get; }

    protected BaseDomainEvent(Guid? correlationId = null)
    {
        CorrelationId = correlationId;
    }
}
