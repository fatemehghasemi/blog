using Blog.Domain.Common;

namespace Blog.Application.Interfaces;

public interface IDomainEventCollector
{
    IReadOnlyCollection<IDomainEvent> CollectAndClear();
}
