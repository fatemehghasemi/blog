using Blog.Application.Interfaces;
using Blog.Infrastructure.Persistence;

namespace Blog.Infrastructure.Events;

/// <summary>
/// The single execution boundary that persists pending changes and then
/// dispatches the domain events raised during the command, guaranteeing events
/// are only published after a successful save.
/// </summary>
internal sealed class UnitOfWorkDomainEventProcessor : IUnitOfWorkDomainEventProcessor
{
    private readonly BlogDbContext _dbContext;
    private readonly IDomainEventCollector _domainEventCollector;
    private readonly IEventDispatcher _eventDispatcher;

    public UnitOfWorkDomainEventProcessor(
        BlogDbContext dbContext,
        IDomainEventCollector domainEventCollector,
        IEventDispatcher eventDispatcher)
    {
        _dbContext = dbContext;
        _domainEventCollector = domainEventCollector;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<int> SaveChangesAndDispatchEventsAsync(CancellationToken cancellationToken = default)
    {
        var affected = await _dbContext.SaveChangesAsync(cancellationToken);

        var domainEvents = _domainEventCollector.CollectAndClear();
        await _eventDispatcher.DispatchAsync(domainEvents, cancellationToken);

        return affected;
    }
}
