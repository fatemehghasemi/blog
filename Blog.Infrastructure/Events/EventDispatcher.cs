using Blog.Application.Interfaces;
using Blog.Domain.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.Events;

/// <summary>
/// Resolves and invokes every <see cref="IDomainEventHandler{TEvent}"/> registered
/// for the runtime type of each domain event.
/// </summary>
internal sealed class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handleMethod = handlerType.GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))!;

            foreach (var handler in _serviceProvider.GetServices(handlerType))
            {
                if (handler is null)
                {
                    continue;
                }

                await (Task)handleMethod.Invoke(handler, new object[] { domainEvent, cancellationToken })!;
            }
        }
    }
}
