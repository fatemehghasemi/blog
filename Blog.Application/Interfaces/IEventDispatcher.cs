using Blog.Domain.Common;

namespace Blog.Application.Interfaces;

public interface IEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}
